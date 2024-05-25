using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{
    public class MacroControlDefinition : ControlDefinition
    {
        public MacroAction[] MacroActions { get; set; }

        public MacroControlDefinition()
        {
            this.ControlType = ControlType.Macro;
            this.MacroActions = Array.Empty<MacroAction>();
        }
    }
}
