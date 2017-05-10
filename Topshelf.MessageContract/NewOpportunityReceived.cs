using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nimbus.MessageContracts;

namespace Topshelf.MessageContract
{
    public class NewOpportunityReceived : IBusEvent
    {
        public string Id { get; set; }
        public string Code { get; set; }

    }
}
