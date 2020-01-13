using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Sales.Application.Events
{
    public class OrderUpdatedEvent : Event
    {
        public OrderUpdatedEvent(Guid clientId, Guid orderId, decimal totalPrice)
        {
            ClientId = clientId;
            OrderId = orderId;
            TotalPrice = totalPrice;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal TotalPrice { get; private set; }

    }
}
