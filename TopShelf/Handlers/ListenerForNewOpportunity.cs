using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nimbus.Handlers;
using Topshelf.MessageContract;

namespace TopShelf.Handlers
{
    public class ListenerForNewOpportunity : IHandleMulticastEvent<NewOpportunityReceived>
    {
        public async Task Handle(NewOpportunityReceived busEvent)
        {
            Console.WriteLine(busEvent.Id);

            Console.WriteLine(busEvent.Code);
        }
    }
}
