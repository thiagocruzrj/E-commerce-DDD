using Ecommerce.Core.DomainObjects;
using Ecommerce.Sales.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Ecommerce.Sales.Domain.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItem => _orderItems;

        // EF Relation
        public virtual Voucher Voucher { get; private set; }

    }
}
