using Ecommerce.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Sales.Application.Commands
{
    public class CancelProcessingOrderCommand : Command
    {
        public CancelProcessingOrderCommand(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
    }
}
