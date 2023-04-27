using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{
    public class BlankControlDefinition : ControlDefinition
    {
        public BlankControlDefinition()
        {
            this.ControlType = ControlType.Blank;
        }
    }
}
