using Ecommerce.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Core.DomainObjects
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = AggregateId;
        }
    }
}
