using Ecommerce.Core.Messages;
using FluentValidation;
using System;

namespace Ecommerce.Sales.Application.Commands
{
    public class RemoveOrderItemCommand : Command
    {
        public RemoveOrderItemCommand(Guid clientId, Guid productId)
        {
            ClientId = clientId;
            ProductId = productId;
        }

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoveOrderItemValidation : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliend Id Invalid");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product Id Invalid");
        }
    }
}
