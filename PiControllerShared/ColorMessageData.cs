using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{
    public class ColorMessageData : MessageData
    {
        public ColorMessageData(Guid controlId, int red, int green, int blue)
        {
            this.ControlId = controlId;
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public Guid ControlId { get; }
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }
    }
}
