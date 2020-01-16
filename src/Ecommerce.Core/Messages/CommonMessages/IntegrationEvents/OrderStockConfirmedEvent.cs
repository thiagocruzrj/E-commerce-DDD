using Ecommerce.Core.DomainObjects.DTO;
using System;

namespace Ecommerce.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderStockConfirmedEvent : IntegrationEvent
    {
        public OrderStockConfirmedEvent(Guid orderId, Guid clientId, decimal total, ListOrderProducts orderProducts, string cardName, string cardNumber, string expirationCard, string cvvCard)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            OrderProducts = orderProducts;
            CardName = cardName;
            CardNumber = cardNumber;
            ExpirationCard = expirationCard;
            CvvCard = cvvCard;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public ListOrderProducts OrderProducts { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpirationCard { get; private set; }
        public string CvvCard { get; private set; }
    }
}
