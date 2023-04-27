using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared
{
    public class ValueMessageData : MessageData
    {
        public ValueMessageData(Guid controlId, int value)
        {
            this.ControlId = controlId;
            this.Value = value;
        }

        public Guid ControlId { get; }
        public int Value { get; }
    }
}
