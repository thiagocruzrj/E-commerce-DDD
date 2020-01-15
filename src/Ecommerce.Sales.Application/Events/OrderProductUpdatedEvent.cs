using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Sales.Application.Events
{
    public class OrderProductUpdatedEvent : Event
    {
        public OrderProductUpdatedEvent(Guid clientId, Guid orderId, Guid productId, int quantity)
        {
            ClientId = clientId;
            AggregateId = orderId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
    }
}
