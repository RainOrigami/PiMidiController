using NAudio.Midi;
using Newtonsoft.Json;
using PiControllerShared;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using TobiasErichsen.teVirtualMIDI;

namespace PiControllerServer
{
    public partial class PiControllerWindow : Form
    {
        private const string definitionsFileName = "definitions.json";

        private BindingList<TabDefiner> tabs = new BindingList<TabDefiner>();
        private List<PageDefinition> definitions;
        private Dictionary<int, MidiEvent> lastEvents = new();
        private Dictionary<int, (int red, int green, int blue)> lastColors = new();

        private Midi midi;
        private Server server;

        public PiControllerWindow()
        {
            InitializeComponent();

            lbTabs.DataSource = tabs;
            lbTabs.DisplayMember = "TabName";

            this.definitions = this.loadDefinitionsFromFile(definitionsFileName).ToList();
            this.fillTabDefinersFromDefinitions(this.definitions.ToArray());

            // Midi
            this.midi = new Midi("Pi Midi Controller");
            this.midi.MidiEventReceived += this.Midi_MidiEventReceived;
            this.midi.ColorEventReceived += this.Midi_ColorEventReceived;

            // Server
            int port = 14817;
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                int.TryParse(Environment.GetCommandLineArgs()[1], out port);
            }
            this.server = new Server(port);
            this.server.ClientConnected += this.Server_ClientConnected;
            this.server.NoteReceived += this.Server_NoteReceived;
        }

        private PageDefinition[] loadDefinitionsFromFile(string file)
        {
            if (!File.Exists(file))
            {
                return Array.Empty<PageDefinition>();
            }

            try
            {
                return JsonConvert.DeserializeObject<PageDefinition[]>(File.ReadAllText(file), new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                }) ?? Array.Empty<PageDefinition>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Array.Empty<PageDefinition>();
            }
        }

        private void saveDefinitionsToFile(string file, PageDefinition[] definitions)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(definitions, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            }));
        }

        private void fillTabDefinersFromDefinitions(PageDefinition[] definitions)
        {
            this.tabs.Clear();
            foreach (PageDefinition definition in definitions.OrderBy(d => d.Index))
            {
                TabDefiner tab = new TabDefiner()
                {
                    Dock = DockStyle.Fill
                };
                tab.FillFromDefinition(definition);
                this.tabs.Add(tab);
            }
        }

        private void showToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PiControllerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                return;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.BringToFront();
        }

        private void btnAddTab_Click(object sender, EventArgs e)
        {
            this.tabs.Add(new TabDefiner() { TabName = "New tab", Dock = DockStyle.Fill });
        }

        private void lbTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTabs.SelectedItem is TabDefiner tab)
            {
                txtTabName.Text = tab.TabName;

                scDefiners.Panel2.Controls.Clear();
                scDefiners.Panel2.Controls.Add(tab);
            }
        }

        private void txtTabName_KeyUp(object sender, KeyEventArgs e)
        {
            if (lbTabs.SelectedItem is TabDefiner tab)
            {
                tab.TabName = txtTabName.Text;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            this.definitions.Clear();

            int index = 0;

            foreach (TabDefiner tab in this.tabs)
            {
                PageDefinition definition = tab.GetDefinition();
                definition.Index = index++;
                this.definitions.Add(definition);
            }

            this.saveDefinitionsToFile(definitionsFileName, this.definitions.ToArray());
            await this.server.SendRaw(new PageDefinitionsMessageData(this.definitions.ToArray()));
            foreach (MidiEvent lastEvent in this.lastEvents.Values.ToArray())
            {
                await relayEvent(lastEvent);
            }
        }

        private async void Server_ClientConnected(object? sender, Guid e)
        {
            await this.server.SendRaw(e, new PageDefinitionsMessageData(this.definitions.ToArray()));
            foreach (MidiEvent lastEvent in this.lastEvents.Values)
            {
                await relayEvent(lastEvent);
            }
        }

        private void Server_NoteReceived(object? sender, (Guid guid, int value) e)
        {
            ControlDefinition? control = this.definitions.SelectMany(d => d.Controls).FirstOrDefault(c => c.Id == e.guid);
            if (control is null)
            {
                Console.WriteLine($"Invalid control id {e.guid}");
                return;
            }

            if (control.ControlType == ControlType.Button)
            {
                if (e.value > 0)
                {
                    this.midi.SendNoteOn(control.Note, e.value);
                }
                else
                {
                    this.midi.SendNoteOff(control.Note);
                }
            }
            else if (control.ControlType == ControlType.Knob)
            {
                this.midi.SendControlChange(control.Note, e.value);
            }
            else
            {
                Console.WriteLine("Invalid control type to receive data from");
            }
        }

        private async void Midi_MidiEventReceived(object? sender, MidiEvent e)
        {
            await relayEvent(e);
        }

        private async void Midi_ColorEventReceived(object? sender, (int note, int red, int green, int blue) e)
        {
            ControlDefinition? control = this.definitions.SelectMany(d => d.Controls).FirstOrDefault(c => c.Note == e.note);
            if (control is null)
            {
                Console.WriteLine($"Invalid control note {e.note}");
                return;
            }

            await this.server.SendColor(control.Id, e.red, e.green, e.blue);

            if (lastColors.ContainsKey(e.note))
            {
                lastColors.Remove(e.note);
            }
            lastColors.Add(e.note, (e.red, e.green, e.blue));
        }

        private async Task relayEvent(MidiEvent e)
        {
            if (e is ControlChangeEvent controlChange)
            {
                ControlDefinition? control = this.definitions.SelectMany(d => d.Controls).FirstOrDefault(c => c.Note == (int)controlChange.Controller);
                if (control is null)
                {
                    Console.WriteLine($"Invalid control note {(int)controlChange.Controller} ({controlChange.Controller})");
                    return;
                }

                await this.server.SendNote(control.Id, controlChange.ControllerValue);

                if (lastEvents.ContainsKey((int)controlChange.Controller))
                {
                    lastEvents.Remove((int)controlChange.Controller);
                }
                lastEvents.Add((int)controlChange.Controller, controlChange);
            }
            else if (e is NoteEvent noteEvent)
            {
                ControlDefinition? control = this.definitions.SelectMany(d => d.Controls).FirstOrDefault(c => c.Note == noteEvent.NoteNumber);
                if (control is null)
                {
                    Console.WriteLine($"Invalid control note {noteEvent.NoteNumber}");
                    return;
                }

                if (noteEvent.CommandCode == MidiCommandCode.NoteOn)
                {
                    await this.server.SendNote(control.Id, noteEvent.Velocity);
                }
                else if (noteEvent.CommandCode == MidiCommandCode.NoteOff)
                {
                    await this.server.SendNote(control.Id, 0);
                }

                if (lastEvents.ContainsKey(noteEvent.NoteNumber))
                {
                    lastEvents.Remove(noteEvent.NoteNumber);
                }
                lastEvents.Add(noteEvent.NoteNumber, noteEvent);
            }
            else
            {
                await Console.Out.WriteLineAsync($"Unhandled midi event: {e.CommandCode}");
            }
        }

        private void btnRemoveTab_Click(object sender, EventArgs e)
        {
            if (this.lbTabs.SelectedItem == null)
            {
                return;
            }

            this.tabs.Remove((TabDefiner)this.lbTabs.SelectedItem);
            scDefiners.Panel2.Controls.Clear();
        }
    }
}
