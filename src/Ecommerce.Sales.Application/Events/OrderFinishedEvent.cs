using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Sales.Application.Events
{
    public class OrderFinishedEvent : Event
    {
        public OrderFinishedEvent(Guid orderId)
        {
            AggregateId = orderId;
            OrderId = orderId;
        }

        public Guid OrderId { get; private set; }
    }
}
