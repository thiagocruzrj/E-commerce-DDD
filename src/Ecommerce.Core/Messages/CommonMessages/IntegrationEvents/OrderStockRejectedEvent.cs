using System;

namespace Ecommerce.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderStockRejectedEvent : IntegrationEvent
    {
        public OrderStockRejectedEvent(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
    }
}
