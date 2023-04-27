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
    public partial class TabDefiner : UserControl, INotifyPropertyChanged
    {
        private string tabName = "New Tab";
        private Guid Guid;
        private ControlDisplay[] displays;
        private PageDefinition definition;

        public string TabName
        {
            get => tabName;
            set
            {
                tabName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TabName)));
            }
        }

        public TabDefiner()
        {
            InitializeComponent();

            displays = new ControlDisplay[]
            {
                controlDisplay1,
                controlDisplay2,
                controlDisplay3,
                controlDisplay4,
                controlDisplay5,
                controlDisplay6,
                controlDisplay7,
                controlDisplay8,
                controlDisplay9,
                controlDisplay10,
                controlDisplay11,
                controlDisplay12,
                controlDisplay13,
                controlDisplay14,
                controlDisplay15,
            };

            this.definition = new PageDefinition()
            {
                Label = this.TabName,
                Id = Guid.NewGuid()
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        internal PageDefinition GetDefinition()
        {
            this.definition.Label = this.TabName;
            this.definition.Controls = displays.Select(d => d.ControlDefiner.GetDefinition()).ToArray();
            for (int i = 0; i < this.definition.Controls.Length; i++)
            {
                this.definition.Controls[i].Index = i;
                if (this.definition.Controls[i].Note == -1)
                {
                    this.definition.Controls[i].Note = this.displays.Length * this.definition.Index + i;
                }
            }

            return this.definition;
        }

        internal void FillFromDefinition(PageDefinition definition)
        {
            this.TabName = definition.Label;
            this.Guid = definition.Id;

            ControlDefinition[] controls = definition.Controls.OrderBy(c => c.Index).ToArray();

            for (int i = 0; i < Math.Min(controls.Length, displays.Length); i++)
            {
                displays[i].LoadFromDefinition(controls[i]);
            }
        }
    }
}
