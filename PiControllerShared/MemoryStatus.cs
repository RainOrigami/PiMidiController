using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared;

public record MemoryStatus(long TotalPhysicalMemory, long AvailablePhysicalMemory);
