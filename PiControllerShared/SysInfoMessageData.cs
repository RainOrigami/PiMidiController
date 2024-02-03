using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared;

public class SysInfoMessageData : MessageData
{
    public SysInfoMessageData(CPUCore[] cpus, MemoryStatus memoryStatus)
    {
        this.Cpus = cpus;
        this.MemoryStatus = memoryStatus;
    }

    public CPUCore[] Cpus { get; }
    public MemoryStatus MemoryStatus { get; }
}
