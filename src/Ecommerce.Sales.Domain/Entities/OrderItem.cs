using Ecommerce.Core.DomainObjects;
using System;

namespace Ecommerce.Sales.Domain.Entities
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }

        // EF Relation
        public Order Order { get; private set; }
    }
}
