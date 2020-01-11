using Ecommerce.Core.Messages.DomainEvents;
using System;

namespace Ecommerce.Catalog.Domain.Events
{
    public class ProductBelowStockEvent : DomainEvent
    {
        public int RemainingQuantity { get; private set; }

        public ProductBelowStockEvent(Guid aggregateId, int remainingQuantity) : base(aggregateId)
        {
            RemainingQuantity = remainingQuantity;
        }
    }
}
