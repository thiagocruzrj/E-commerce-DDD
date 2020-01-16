using Ecommerce.Core.Messages;
using System;

namespace Ecommerce.Sales.Application.Commands
{
    public class CancelProcessingOrderReverseStockCommand : Command
    {
        public CancelProcessingOrderReverseStockCommand(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
    }
}
