using PiControllerShared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiControllerServer
{
    public partial class MacroDefinerDialog : Form
    {
        private static readonly Dictionary<Keys, string> specialKeys = new()
        {
            { Keys.Back, "BACKSPACE" },
            { Keys.CapsLock, "CAPSLOCK" },
            { Keys.Delete, "DELETE" },
            { Keys.Down, "DOWN" },
            { Keys.End, "END" },
            { Keys.Enter, "ENTER" },
            { Keys.Escape, "ESC" },
            { Keys.Home, "HOME" },
            { Keys.Insert, "INSERT" },
            { Keys.Left, "LEFT" },
            { Keys.PageDown, "PGDN" },
            { Keys.PageUp, "PGUP" },
            { Keys.PrintScreen, "PRTSC" },
            { Keys.Right, "RIGHT" },
            { Keys.Scroll, "SCROLLLOCK" },
            { Keys.Tab, "TAB" },
            { Keys.Add, "+" },
            { Keys.D6 | Keys.Shift, "^" },
            { Keys.D5 | Keys.Shift, "%" },
            { (Keys)219 | Keys.Shift, "{" },
            { (Keys)221 | Keys.Shift, "}" },
            { (Keys)219, "[" },
            { (Keys)221, "]" },
            { (Keys)192 | Keys.Shift, "~" }
        };

        public MacroDefinerDialog()
        {
            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;

            this.cbAvailableKeys.Items.AddRange(Enum.GetNames(typeof(Keys)));
        }

        public MacroAction GetMacroAction()
        {
            if (this.rbKey.Checked)
            {
                return new KeyMacroAction()
                {
                    Key = (short)Enum.Parse<Keys>(this.cbAvailableKeys.SelectedItem.ToString()!)
                };
            }
            else
            {
                return new DelayMacroAction()
                {
                    Delay = (int)this.nudDelay.Value
                };
            }
        }

        public void LoadFromMacroAction(MacroAction macroAction)
        {
            if (macroAction is KeyMacroAction keyMacroAction)
            {
                this.rbKey.Checked = true;
                this.cbAvailableKeys.SelectedItem = keyMacroAction.Key.ToString();
                //this.txtKey.Text = keyMacroAction.Key;
            }
            else if (macroAction is DelayMacroAction delayMacroAction)
            {
                this.rbDelay.Checked = true;
                this.nudDelay.Value = delayMacroAction.Delay;
            }
        }

        private void rbTypes_CheckedChanged(object sender, EventArgs e)
        {
            this.gbKey.Enabled = this.rbKey.Checked;
            this.gbDelay.Enabled = this.rbDelay.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //private void btnAddStroke_Click(object sender, EventArgs e)
        //{
        //    if (this.cbAvailableKeys.SelectedItem == null)
        //    {
        //        return;
        //    }

        //    if (specialKeys.TryGetValue(Enum.Parse<Keys>(this.cbAvailableKeys.SelectedItem.ToString()!), out string? specialKey))
        //    {
        //        this.txtKey.Text += $"{{{specialKey}}}";
        //    }
        //    else
        //    {
        //        this.txtKey.Text += $"{{{this.cbAvailableKeys.SelectedItem}}}";
        //    }
        //}
    }
}
