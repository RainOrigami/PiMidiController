using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{
    public class KnobControlDefinition : ControlDefinition
    {
        public KnobControlDefinition()
        {
            ControlType = ControlType.Knob;
        }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Overdrive { get; set; }
        public int[] SoftStops { get; set; } = new int[0];
        public bool Centered { get; set; } 
    }
}
