using Ecommerce.Core.DomainObjects.DTO;
using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ProcessOrderCanceledEvent : Event
    {
        public ProcessOrderCanceledEvent(Guid orderId, Guid clientId, ListOrderProducts listOrderProducts)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            ListOrderProducts = listOrderProducts;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public ListOrderProducts ListOrderProducts { get; private set; }
    }
}
