using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Sales.Application.Events
{
    public class OrderUpdatedEvent : Event
    {
        public OrderUpdatedEvent(Guid clientId, Guid orderId, decimal totalValue)
        {
            ClientId = clientId;
            OrderId = orderId;
            TotalValue = totalValue;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal TotalValue { get; private set; }

    }
}
