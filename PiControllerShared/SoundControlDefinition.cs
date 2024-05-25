using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{
    public class SoundControlDefinition : ControlDefinition
    {
        public SoundControlDefinition()
        {
            ControlType = ControlType.Sound;
        }

        public string? SoundPath { get; set; }
        public bool Toggle { get; set; } = true;
    }
}
