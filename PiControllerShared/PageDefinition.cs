using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{
    public class PageDefinition
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public int Index { get; set; }
        public ControlDefinition[] Controls { get; set; } = new ControlDefinition[15];
    }
}
