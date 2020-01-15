using System;

namespace Ecommerce.Core.DomainObjects.DTO
{
    public class PaymentOrder
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal Total { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationCard { get; set; }
        public string CvvCard { get; set; }
    }
}
