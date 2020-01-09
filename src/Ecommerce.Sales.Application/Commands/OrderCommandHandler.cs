using Ecommerce.Core.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
    {
        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            return true;
            
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;
            foreach (var error in message.ValidationResult.Errors)
            {
                // launching an error event
            }

            return false;
        }
    }
}
