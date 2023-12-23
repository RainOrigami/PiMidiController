using Hardware.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared;

public class SysInfoMessageData : MessageData
{
    public SysInfoMessageData(IHardwareInfo hardwareInfo)
    {
        this.HardwareInfo = hardwareInfo;
    }

    public IHardwareInfo HardwareInfo { get; }
}
