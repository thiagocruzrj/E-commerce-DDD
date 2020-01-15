using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Sales.Application.Events
{
    public class OrderVoucherAppliedEvent : Event
    {
        public OrderVoucherAppliedEvent(Guid clientId, Guid orderId, Guid productId)
        {
            ClientId = clientId;
            AggregateId = orderId;
            OrderId = orderId;
            ProductId = productId;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
    }
}
