using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{
    public class ControlDefinition
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public int Index { get; set; }
        public ControlType ControlType { get; set; } = ControlType.Blank;
        public int Note { get; set; }
    }
}
