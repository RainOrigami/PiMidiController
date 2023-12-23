using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerShared;

public class PageDefinitionsMessageData : MessageData
{
    public PageDefinitionsMessageData(PageDefinition[] definitions)
    {
        this.Definitions = definitions;
    }

    public PageDefinition[] Definitions { get; }
}
