using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{

    public class ButtonControlDefinition : ControlDefinition
    {
        public ButtonControlDefinition()
        {
            ControlType = ControlType.Button;
        }

        public bool Toggle { get; set; }
    }
}
