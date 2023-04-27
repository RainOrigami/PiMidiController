﻿
using Gtk;
using Newtonsoft.Json;
using PiControllerShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PiControllerClient
{
    public class MainWindow : Window
    {
        private Connection? connection;
        private Notebook notebook;
        private Dictionary<ControlDefinition, Widget> allControls = new Dictionary<ControlDefinition, Widget>();

        public MainWindow(string host, int port) : base(WindowType.Toplevel)
        {
            this.Shown += this.MainWindow_Shown;

            Gtk.Application.Invoke(delegate { initialise(); });
            this.host = host;
            this.port = port;
        }

        private void initialise()
        {
            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PiControllerClient.style.css"))
            {
                if (stream == null)
                {
                    throw new Exception("Stylesheet not found");
                }

                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string css = System.Text.Encoding.UTF8.GetString(buffer);
                CssProvider cssProvider = new CssProvider();
                cssProvider.LoadFromData(css);
                StyleContext.AddProviderForScreen(Gdk.Screen.Default, cssProvider, Gtk.StyleProviderPriority.User);
            }

            SetDefaultSize(600, 400);
#if !DEBUG
            Fullscreen();
#endif

            notebook = new Notebook()
            {
                Hexpand = true,
                Vexpand = true,
                Halign = Align.Fill,
                Valign = Align.Fill
            };
            Add(notebook);
            notebook.Show();
        }

        private void MainWindow_Shown(object? sender, EventArgs e)
        {
#if !DEBUG
            Window.Cursor = new Gdk.Cursor(Gdk.CursorType.BlankCursor);
#endif

            Console.WriteLine("Ready and connecting");
            this.connection = new Connection(host, port);
            this.connection.PageDefinitionsReceived += this.Connection_PageDefinitionsReceived;
            this.connection.ValueReceived += this.Connection_ValueReceived;
            this.connection.ColorReceived += this.Connection_ColorReceived;
        }

        private void Connection_ColorReceived(object? sender, (Guid controlId, int red, int green, int blue) e)
        {
            Application.Invoke(delegate { updateColor(e.controlId, (byte)(e.red * 2), (byte)(e.green * 2), (byte)(e.blue * 2)); });
        }

        private void Connection_ValueReceived(object? sender, (Guid controlId, int value) e)
        {
            Application.Invoke(delegate { updateValue(e.controlId, e.value); });
        }

        private void updateValue(Guid controlId, int value)
        {
            foreach (Widget control in allControls.Where(kvp => kvp.Key.Id == controlId).Select(kvp => kvp.Value))
            {
                if (control is TurnKnob turnKnob)
                {
                    turnKnob.Value = value;
                }
            }
        }

        Dictionary<Guid, CssProvider> activeProviders = new();
        private readonly string host;
        private readonly int port;

        private void updateColor(Guid controlId, byte red, byte green, byte blue)
        {
            foreach (KeyValuePair<ControlDefinition, Widget> control in allControls.Where(kvp => kvp.Key.Id == controlId))
            {
                if (control.Value is Button button)
                {
                    byte foregroundRed = 255;
                    byte foregroundGreen = 255;
                    byte foregroundBlue = 255;
                    int brightness = (int)Math.Round((red * 299.0 + green * 587 + blue * 114) / 1000);
                    if (brightness > 125)
                    {
                        foregroundRed = 0;
                        foregroundGreen = 0;
                        foregroundBlue = 0;
                    }

                    if (this.activeProviders.ContainsKey(controlId))
                    {
                        button.StyleContext.RemoveProvider(this.activeProviders[controlId]);
                        this.activeProviders.Remove(controlId);
                    }

                    CssProvider cssProvider = new CssProvider();
                    cssProvider.LoadFromData($".note-{control.Key.Note} {{ background-color: rgb({red}, {green}, {blue}); color: rgb({foregroundRed}, {foregroundGreen}, {foregroundBlue}); }}");
                    button.StyleContext.AddProvider(cssProvider, Gtk.StyleProviderPriority.User);
                    this.activeProviders.Add(controlId, cssProvider);
                }
            }
        }

        private void Connection_PageDefinitionsReceived(object? sender, PageDefinition[] definitions)
        {
            Gtk.Application.Invoke(delegate { updatePages(definitions); });
        }

        private void updatePages(PageDefinition[] definitions)
        {
            Console.WriteLine($"Window size is {AllocatedWidth}x{AllocatedHeight}");

            int rows = 3;
            int cols = 5;

            Console.WriteLine($"Removing {notebook.NPages} notebook pages...");
            while (notebook.NPages > 0)
            {
                notebook.RemovePage(0);
            }

            Grid[] panels = new Grid[definitions.Length];

            foreach (PageDefinition definition in definitions.OrderBy(d => d.Index))
            {
                var page = new Grid()
                {
                    Hexpand = true,
                    Vexpand = true,
                    Halign = Align.Fill,
                    Valign = Align.Fill,
                    ColumnHomogeneous = true,
                    RowHomogeneous = true,
                    ColumnSpacing = 12,
                    RowSpacing = 12
                };

                Console.WriteLine($"Adding page {definition.Label}");
                notebook.AppendPage(page, new Label(definition.Label));
                page.Show();

                ControlDefinition[] controls = definition.Controls.OrderBy(c => c.Index).ToArray();

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        int buttonNumber = row * cols + col;

                        if (buttonNumber >= controls.Length)
                        {
                            continue;
                        }

                        ControlDefinition controlDefinition = controls[buttonNumber];

                        Widget? control = null;

                        if (controlDefinition.ControlType == ControlType.Knob && controlDefinition is KnobControlDefinition knobControlDefinition)
                        {
                            TurnKnob turnKnob = new(knobControlDefinition.Label, knobControlDefinition.Min, knobControlDefinition.Max, knobControlDefinition.Overdrive, knobControlDefinition.SoftStops, knobControlDefinition.Centered)
                            {
                                Vexpand = true,
                                Hexpand = true,
                                Value = 100
                            };

                            turnKnob.ValueChanged += (sender, args) => OnKnobTurned(sender, args, controlDefinition.Id);
                            control = turnKnob;
                        }
                        else if (controlDefinition.ControlType == ControlType.Button && controlDefinition is ButtonControlDefinition buttonControlDefinition)
                        {
                            Button button = new Button();
                            DynamicSizeLabel label = new DynamicSizeLabel(buttonControlDefinition.Label)
                            {
                                MaximumFontSize = 36
                            };
                            button.Add(label);
                            label.Show();

                            button.StyleContext.AddClass($"note-{controlDefinition.Note}");
                            button.Pressed += (sender, args) => OnButtonPressed(controlDefinition.Id);
                            button.Released += (sender, args) => OnButtonReleased(controlDefinition.Id);
                            control = button;
                        }

                        if (control != null)
                        {
                            allControls.Add(controlDefinition, control);
                            page.Attach(control, col + 1, row, 1, 1);
                            control.Show();
                        }
                    }
                }
            }
            Console.WriteLine("Updating UI");
        }

        private async void OnButtonPressed(Guid controlId)
        {
            if (this.connection == null)
            {
                return;
            }

            await this.connection.SendButton(controlId, true);
        }

        private async void OnButtonReleased(Guid controlId)
        {
            if (this.connection == null)
            {
                return;
            }

            await this.connection.SendButton(controlId, false);
        }

        private async void OnKnobTurned(object? sender, EventArgs args, Guid controlId)
        {
            if (this.connection == null)
            {
                return;
            }

            if (sender is not TurnKnob turnKnob)
            {
                return;
            }

            await this.connection.SendKnob(controlId, turnKnob.Value);
        }

        protected override bool OnDeleteEvent(Gdk.Event ev)
        {
            Application.Quit();
            return true;
        }
    }
}
