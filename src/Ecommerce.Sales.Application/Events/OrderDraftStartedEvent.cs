using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Sales.Application.Events
{
    public class OrderDraftStartedEvent : Event
    {
        public OrderDraftStartedEvent(Guid clientId, Guid orderId)
        {
            ClientId = clientId;
            OrderId = orderId;
            AggregateId = orderId;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
    }
}
