using PiControllerShared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiControllerServer
{
    public partial class ControlDefiner : UserControl
    {
        private ControlDefinition definition;

        public ControlDefiner()
        {
            InitializeComponent();

            definition = new BlankControlDefinition()
            {
                Id = Guid.NewGuid()
            };
        }

        internal void LoadFromDefinition(ControlDefinition controlDefinition)
        {
            this.definition = controlDefinition;
            this.txtName.Text = controlDefinition.Label;
            if (controlDefinition.Note < this.nudNote.Minimum || controlDefinition.Note > this.nudNote.Maximum)
            {
                controlDefinition.Note = -1;
            }

            this.nudNote.Value = controlDefinition.Note;
            switch (controlDefinition.ControlType)
            {
                case ControlType.Blank:
                    rbTypeBlank.Checked = true;
                    break;
                case ControlType.Button:
                    rbTypeButton.Checked = true;
                    break;
                case ControlType.Knob:
                    rbTypeKnob.Checked = true;
                    gbSoftStops.Enabled = true;
                    gbKnobSettings.Enabled = true;

                    KnobControlDefinition knobControlDefinition = (KnobControlDefinition)controlDefinition;
                    nudMinValue.Value = knobControlDefinition.Min;
                    nudMaxValue.Value = knobControlDefinition.Max;
                    nudOverdrive.Value = knobControlDefinition.Overdrive;
                    cbCentered.Checked = knobControlDefinition.Centered;
                    lbSoftStops.Items.Clear();
                    foreach (int softStop in knobControlDefinition.SoftStops)
                    {
                        lbSoftStops.Items.Add(softStop);
                    }

                    break;
                case ControlType.Sound:
                    rbTypeSound.Checked = true;
                    txtSoundPath.Text = ((SoundControlDefinition)controlDefinition).SoundPath;
                    cbToggleSound.Checked = ((SoundControlDefinition)controlDefinition).Toggle;
                    break;
                case ControlType.Macro:
                    rbTypeMacro.Checked = true;
                    MacroControlDefinition macroControlDefinition = (MacroControlDefinition)controlDefinition;
                    lbMacros.Items.Clear();
                    lbMacros.Items.AddRange(macroControlDefinition.MacroActions);
                    break;
                default:
                    rbTypeBlank.Checked = true;
                    break;
            }
            nudNote.Value = controlDefinition.Note;
        }

        internal ControlDefinition GetDefinition()
        {
            ControlType controlType = rbTypeBlank.Checked ? ControlType.Blank : rbTypeButton.Checked ? ControlType.Button : rbTypeKnob.Checked ? ControlType.Knob : rbTypeSound.Checked ? ControlType.Sound : ControlType.Macro;

            if (this.definition.ControlType != controlType)
            {
                Guid guid = this.definition.Id;
                this.definition = controlType switch
                {
                    ControlType.Blank => new BlankControlDefinition(),
                    ControlType.Button => new ButtonControlDefinition(),
                    ControlType.Knob => new KnobControlDefinition()
                    {
                        Min = (int)nudMinValue.Value,
                        Max = (int)nudMaxValue.Value,
                        Overdrive = (int)nudOverdrive.Value,
                        SoftStops = this.lbSoftStops.Items.OfType<int>().OrderBy(x => x).ToArray(),
                        Centered = cbCentered.Checked,
                    },
                    ControlType.Sound => new SoundControlDefinition()
                    {
                        SoundPath = string.IsNullOrEmpty(txtSoundPath.Text) ? null : txtSoundPath.Text,
                        Toggle = cbToggleSound.Checked
                    },
                    ControlType.Macro => new MacroControlDefinition()
                    {
                        MacroActions = this.lbMacros.Items.OfType<MacroAction>().ToArray()
                    },
                    _ => new BlankControlDefinition(),
                };
                this.definition.Id = guid;
            }

            this.definition.Label = txtName.Text;
            this.definition.Note = (int)nudNote.Value;

            switch (controlType)
            {
                case ControlType.Knob:
                    KnobControlDefinition knobDefinition = (KnobControlDefinition)definition;
                    knobDefinition.Min = (int)nudMinValue.Value;
                    knobDefinition.Max = (int)nudMaxValue.Value;
                    knobDefinition.Overdrive = (int)nudOverdrive.Value;
                    knobDefinition.SoftStops = this.lbSoftStops.Items.OfType<int>().OrderBy(x => x).ToArray();
                    knobDefinition.Centered = cbCentered.Checked;
                    break;
                case ControlType.Sound:
                    SoundControlDefinition soundDefinition = (SoundControlDefinition)definition;
                    soundDefinition.SoundPath = txtSoundPath.Text;
                    soundDefinition.Note = -1;
                    break;
                case ControlType.Macro:
                    MacroControlDefinition macroDefinition = (MacroControlDefinition)definition;
                    macroDefinition.MacroActions = this.lbMacros.Items.OfType<MacroAction>().ToArray();
                    macroDefinition.Note = -1;
                    break;
                default:
                    break;
            }

            return this.definition;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lbSoftStops.Items.Contains((int)nudSoftStop.Value))
            {
                return;
            }

            this.lbSoftStops.Items.Add((int)nudSoftStop.Value);
            int[] values = this.lbSoftStops.Items.OfType<int>().OrderBy(x => x).ToArray();
            this.lbSoftStops.Items.Clear();
            for (int i = 0; i < values.Length; i++)
            {
                this.lbSoftStops.Items.Add(values[i]);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lbSoftStops.SelectedItem == null)
            {
                return;
            }

            this.lbSoftStops.Items.Remove(this.lbSoftStops.SelectedItem);
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            gbSoftStops.Enabled = rbTypeKnob.Checked;
            gbKnobSettings.Enabled = rbTypeKnob.Checked;
            gbSoundSettings.Enabled = rbTypeSound.Checked;
            gbNoteSettings.Enabled = !rbTypeSound.Checked;
            gbMacroSettings.Enabled = rbTypeMacro.Checked;
        }

        private void btnBrowseSound_Click(object sender, EventArgs e)
        {
            OpenFileDialog soundBrowser = new OpenFileDialog()
            {
                Filter = "WAV Files|*.wav"
            };

            if (soundBrowser.ShowDialog() == DialogResult.OK)
            {
                txtSoundPath.Text = soundBrowser.FileName;
            }
        }

        private void btnAddMacro_Click(object sender, EventArgs e)
        {
            MacroDefinerDialog macroDefiner = new MacroDefinerDialog();
            if (macroDefiner.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            MacroAction macroAction = macroDefiner.GetMacroAction();
            this.lbMacros.Items.Add(macroAction);
        }

        private void btnRemoveMacro_Click(object sender, EventArgs e)
        {
            if (this.lbMacros.SelectedItem == null)
            {
                return;
            }

            this.lbMacros.Items.RemoveAt(this.lbMacros.SelectedIndex);
        }

        private void btnUpMacro_Click(object sender, EventArgs e)
        {
            if (this.lbMacros.SelectedItem == null)
            {
                return;
            }

            int index = this.lbMacros.SelectedIndex;
            if (index == 0)
            {
                return;
            }

            object item = this.lbMacros.SelectedItem;
            this.lbMacros.Items.RemoveAt(index);
            this.lbMacros.Items.Insert(index - 1, item);
            this.lbMacros.SelectedIndex = index - 1;
            this.lbMacros.Focus();
        }

        private void btnDownMacro_Click(object sender, EventArgs e)
        {
            if (this.lbMacros.SelectedItem == null)
            {
                return;
            }

            int index = this.lbMacros.SelectedIndex;
            if (index == this.lbMacros.Items.Count - 1)
            {
                return;
            }

            object item = this.lbMacros.SelectedItem;
            this.lbMacros.Items.RemoveAt(index);
            this.lbMacros.Items.Insert(index + 1, item);
            this.lbMacros.SelectedIndex = index + 1;
            this.lbMacros.Focus();
        }

        private void lbMacros_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lbMacros.SelectedItem == null)
            {
                return;
            }

            MacroDefinerDialog macroDefiner = new MacroDefinerDialog();
            macroDefiner.LoadFromMacroAction((MacroAction)this.lbMacros.SelectedItem);
            if (macroDefiner.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            MacroAction macroAction = macroDefiner.GetMacroAction();
            int index = this.lbMacros.SelectedIndex;
            this.lbMacros.Items.RemoveAt(index);
            this.lbMacros.Items.Insert(index, macroAction);
            this.lbMacros.SelectedIndex = index;
            this.lbMacros.Focus();
        }
    }
}
