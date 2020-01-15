using Ecommerce.Core.DomainObjects;
using System;

namespace Ecommerce.Payments.Business.Entities
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public decimal Value { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationCard { get; set; }
        public string CvvCard { get;  set; }

        // EF. Rel.
        public Transaction Transaction { get; set; }
    }
}
