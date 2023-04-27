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

            gbSoftStops.Enabled = false;
            gbKnobSettings.Enabled = false;
        }

        internal void LoadFromDefinition(ControlDefinition controlDefinition)
        {
            this.definition = controlDefinition;
            this.txtName.Text = controlDefinition.Label;
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
                    lbSoftStops.SelectedIndex = 0;

                    break;
                default:
                    rbTypeBlank.Checked = true;
                    break;
            }
            nudNote.Value = controlDefinition.Note;
        }

        internal ControlDefinition GetDefinition()
        {
            ControlType controlType = rbTypeBlank.Checked ? ControlType.Blank : rbTypeButton.Checked ? ControlType.Button : ControlType.Knob;

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

        private void rbTypeKnob_CheckedChanged(object sender, EventArgs e)
        {
            gbSoftStops.Enabled = rbTypeKnob.Checked;
            gbKnobSettings.Enabled = rbTypeKnob.Checked;
        }
    }
}
