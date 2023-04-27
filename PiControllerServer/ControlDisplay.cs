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
    public partial class ControlDisplay : UserControl
    {
        public ControlDefiner ControlDefiner => this.controlDefinerDialog.Definer;
        private ControlDefinerDialog controlDefinerDialog = new ControlDefinerDialog();

        public ControlDisplay()
        {
            InitializeComponent();
        }

        public void LoadFromDefinition(ControlDefinition controlDefinition)
        {
            this.lblNameValue.Text = controlDefinition.Label;
            this.lblNoteValue.Text = controlDefinition.Note.ToString();
            this.lblTypeValue.Text = controlDefinition.ControlType.ToString();
            controlDefinerDialog.Definer.LoadFromDefinition(controlDefinition);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            controlDefinerDialog.ShowDialog();
            LoadFromDefinition(this.controlDefinerDialog.Definer.GetDefinition());
        }
    }
}
