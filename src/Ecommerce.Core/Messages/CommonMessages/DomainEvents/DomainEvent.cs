using System;

namespace Ecommerce.Core.Messages.DomainEvents
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = AggregateId;
        }
    }
}
