using Ecommerce.Core.DomainObjects;
using Ecommerce.Payments.Business.Enums;
using System;

namespace Ecommerce.Payments.Business.Entities
{
    public class Transaction : Entity
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Total { get; set; }
        public StatusTransaction StatusTransaction { get; set; }

        // EF. Rel.
        public Payment Payment { get; set; }
    }
}