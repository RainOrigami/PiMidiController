using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{
    public abstract class MacroAction
    {
    }

    public class KeyMacroAction : MacroAction
    {
        public short Key { get; set; }

        public override string ToString() => $"Key: {Key}";
    }

    public class DelayMacroAction : MacroAction
    {
        public int Delay { get; set; }

        public override string ToString() => $"Delay: {Delay}ms";
    }
}
