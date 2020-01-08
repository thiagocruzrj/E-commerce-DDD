using Ecommerce.Core.DomainObjects;
using Ecommerce.Sales.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Ecommerce.Sales.Domain.Entities
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public VoucherDiscountType VoucherDiscountType { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? DateUse { get; private set; }
        public DateTime DateValidate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        // EF Rel.
        public ICollection<Order> Orders { get; set; }
    }
}
