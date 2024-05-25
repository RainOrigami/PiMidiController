using NAudio.CoreAudioApi;
using NAudio.Midi;
using NAudio.Wave;
using Newtonsoft.Json;
using PiControllerShared;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Management;
using System.Media;
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
        private WasapiOut? outputDevice = null;

        private Midi midi;
        private Server server;

        public PiControllerWindow()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                while (true)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    CPUCore[] cpuCores = this.getCPUUsage();
                    Debug.WriteLine($"Refreshed cpu list in {stopwatch.ElapsedMilliseconds}ms");
                    stopwatch.Restart();
                    MemoryStatus memoryStatus = this.getMemoryStatus();
                    Debug.WriteLine($"Refreshed mem status in {stopwatch.ElapsedMilliseconds}ms");
                    stopwatch.Restart();
                    this.server?.SendRaw(new SysInfoMessageData(cpuCores, memoryStatus));
                    Debug.WriteLine($"Sent hardware info in {stopwatch.ElapsedMilliseconds}ms");

                    await Task.Delay(500);
                }
            });

            lbTabs.DataSource = tabs;
            lbTabs.DisplayMember = "TabName";

            this.definitions = this.loadDefinitionsFromFile(definitionsFileName).ToList();
            this.fillTabDefinersFromDefinitions(this.definitions.ToArray());

            // Midi
            this.midi = new Midi(ConfigurationManager.AppSettings["MidiDeviceName"] ?? "Pi Midi Controller");
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

            if (outputDevice is not null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }

            foreach (TabDefiner tab in this.tabs)
            {
                PageDefinition definition = tab.GetDefinition();
                definition.Index = index++;
                this.definitions.Add(definition);

                foreach (ControlDefinition control in definition.Controls)
                {
                    if (control.ControlType == ControlType.Sound)
                    {
                        SoundControlDefinition soundControl = (SoundControlDefinition)control;
                        if (!File.Exists(soundControl.SoundPath))
                        {
                            continue;
                        }

                        if (!soundControl.SoundPath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        if (soundPlayers.ContainsKey(soundControl.Id))
                        {
                            AudioFileReader currentAudioFile = soundPlayers[soundControl.Id];
                            soundPlayers.Remove(soundControl.Id);
                            currentAudioFile.Dispose();
                        }

                        AudioFileReader newAudioFile = new(soundControl.SoundPath);
                        soundPlayers.Add(soundControl.Id, newAudioFile);
                    }
                }
            }

            this.saveDefinitionsToFile(definitionsFileName, this.definitions.ToArray());
            this.server.SendRaw(new PageDefinitionsMessageData(this.definitions.ToArray()));
            foreach (MidiEvent lastEvent in this.lastEvents.Values.ToArray())
            {
                await relayEvent(lastEvent);
            }
        }

        private async void Server_ClientConnected(object? sender, EventArgs e)
        {
            this.server.SendRaw(new SysInfoMessageData(this.getCPUUsage(), this.getMemoryStatus()));
            this.server.SendRaw(new PageDefinitionsMessageData(this.definitions.ToArray()));
            foreach (MidiEvent lastEvent in this.lastEvents.Values)
            {
                await relayEvent(lastEvent);
            }
        }

        private bool soundToggled = false;
        private Guid? currentToggleSound = null;

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
            else if (control.ControlType == ControlType.Sound)
            {
                SoundControlDefinition soundControl = (SoundControlDefinition)control;
                string? soundPath = soundControl.SoundPath;
                if (string.IsNullOrEmpty(soundPath))
                {
                    Debug.WriteLine("No sound path defined");
                    return;
                }

                if (!File.Exists(soundPath))
                {
                    Debug.WriteLine($"Sound file not found: {soundPath}");
                    return;
                }

                if (!soundPath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Invalid sound file type");
                    return;
                }

                bool toggleOn = soundControl.Toggle && e.value == 127 && !this.soundToggled;
                bool toggleOff = soundControl.Toggle && e.value == 127 && this.soundToggled;

                if (toggleOn)
                {
                    this.soundToggled = true;
                }
                else if (toggleOff)
                {
                    this.soundToggled = false;
                }

                if (soundPlayers.ContainsKey(e.guid) && (!soundControl.Toggle || toggleOff || toggleOn))
                {
                    this.outputDevice?.Stop();
                    AudioFileReader currentAudioFile = soundPlayers[e.guid];
                    currentAudioFile.Seek(0, SeekOrigin.Begin);
                }

                if (e.value == 0 || toggleOff)
                {
                    return;
                }

                if (!soundPlayers.ContainsKey(e.guid))
                {
                    AudioFileReader newAudioFile = new(soundPath);
                    if (this.outputDevice is not null)
                    {
                        this.outputDevice.Stop();
                        this.outputDevice.Dispose();
                    }
                    soundPlayers.Add(e.guid, newAudioFile);
                }

                if (this.outputDevice is null)
                {
                    this.outputDevice = new WasapiOut(AudioClientShareMode.Shared, 0);
                    this.outputDevice.Init(soundPlayers[e.guid]);
                }
                this.outputDevice = new WasapiOut(AudioClientShareMode.Shared, 0);
                this.outputDevice.Init(soundPlayers[e.guid]);

                this.outputDevice.Play();
            }
            else if (control.ControlType == ControlType.Macro)
            {
                MacroAction[] macroActions = (control as MacroControlDefinition)?.MacroActions ?? Array.Empty<MacroAction>();

                if (e.value == 0)
                {
                    return;
                }

                Task.Run(() =>
                {
                    foreach (MacroAction action in macroActions)
                    {
                        if (action is KeyMacroAction keyAction)
                        {
                            GlobalHotkeySender.KeyPress(keyAction.Key);
                            //SendKeys.SendWait(keyAction.Key);
                        }
                        else if (action is DelayMacroAction delayAction)
                        {
                            Thread.Sleep(delayAction.Delay);
                        }
                    }
                });
            }
            else
            {
                Console.WriteLine("Invalid control type to receive data from");
            }
        }

        private Dictionary<Guid, AudioFileReader> soundPlayers = new();

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

            this.server.SendColor(control.Id, e.red, e.green, e.blue);

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
                if (controlChange.Controller is MidiController.AllNotesOff or MidiController.ResetAllControllers)
                {
                    return;
                }

                ControlDefinition? control = this.definitions.SelectMany(d => d.Controls).FirstOrDefault(c => c.Note == (int)controlChange.Controller);
                if (control is null)
                {
                    Console.WriteLine($"Invalid control note {(int)controlChange.Controller} ({controlChange.Controller})");
                    return;
                }

                this.server.SendNote(control.Id, controlChange.ControllerValue);

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
                    this.server.SendNote(control.Id, noteEvent.Velocity);
                }
                else if (noteEvent.CommandCode == MidiCommandCode.NoteOff)
                {
                    this.server.SendNote(control.Id, 0);
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

        PerformanceCounter totalCpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        PerformanceCounter[] cpuCounters = new PerformanceCounter[Environment.ProcessorCount];

        private CPUCore[] getCPUUsage()
        {
            List<CPUCore> cpuCores = new List<CPUCore>();

            int coreCount = Environment.ProcessorCount;
            for (int coreIndex = 0; coreIndex < coreCount; coreIndex++)
            {
                if (cpuCounters[coreIndex] == null)
                {
                    cpuCounters[coreIndex] = new PerformanceCounter("Processor", "% Processor Time", coreIndex.ToString());
                }

                CPUCore core = new CPUCore($"{coreIndex}", cpuCounters[coreIndex].NextValue());

                cpuCores.Add(core);
            }

            return cpuCores.ToArray();
        }

        ManagementObjectSearcher searcher = new ManagementObjectSearcher(new ObjectQuery("SELECT * FROM Win32_OperatingSystem"));
        private MemoryStatus getMemoryStatus()
        {
            long totalPhysicalMemory = 0;
            long availablePhysicalMemory = 0;

            try
            {
                ManagementObjectCollection results = searcher.Get();

                foreach (ManagementObject result in results)
                {
                    totalPhysicalMemory += Convert.ToInt64(result["TotalVisibleMemorySize"]) * 1024; // Convert from kilobytes to bytes
                    availablePhysicalMemory += Convert.ToInt64(result["FreePhysicalMemory"]) * 1024; // Convert from kilobytes to bytes
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving memory information: {ex.Message}");
            }

            return new(totalPhysicalMemory, availablePhysicalMemory);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lbTabs.SelectedItem is not TabDefiner tab)
            {
                MessageBox.Show("WTF? :D");
                return;
            }

            int index = lbTabs.SelectedIndex;
            if (index <= 0)
            {
                return;
            }

            tabs.RemoveAt(index);
            tabs.Insert(index - 1, tab);
            lbTabs.SelectedIndex = index - 1;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lbTabs.SelectedItem is not TabDefiner tab)
            {
                MessageBox.Show("WTF? :D");
                return;
            }

            int index = lbTabs.SelectedIndex;
            if (index >= tabs.Count - 1)
            {
                return;
            }

            tabs.RemoveAt(index);
            tabs.Insert(index + 1, tab);
            lbTabs.SelectedIndex = index + 1;
        }
    }
}
