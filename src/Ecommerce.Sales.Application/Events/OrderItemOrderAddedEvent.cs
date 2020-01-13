using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Sales.Application.Events
{
    public class OrderItemOrderAddedEvent : Event
    {
        public OrderItemOrderAddedEvent(Guid clientId, Guid orderId, Guid productId, decimal unitPrice, int quantity)
        {
            ClientId = clientId;
            OrderId = orderId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
    }
}
