using Ecommerce.Core.DomainObjects.DTO;
using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Sales.Application.Events
{
    public class StartedOrderEvent : Event
    {
        public StartedOrderEvent(Guid orderId, Guid client, decimal total, ListOrderProducts orderProducts, string cardName, string cardNumber, string expirationDate, string cvvCard)
        {
            OrderId = orderId;
            Client = client;
            Total = total;
            OrderProducts = orderProducts;
            CardName = cardName;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            CvvCard = cvvCard;
        }

        public Guid OrderId { get; private set; }
        public Guid Client { get; private set; }
        public decimal Total { get; private set; }
        public ListOrderProducts OrderProducts { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpirationDate { get; private set; }
        public string CvvCard { get; private set; }
    }
}
