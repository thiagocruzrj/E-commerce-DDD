using Ecommerce.Core.Messages;
using FluentValidation;
using System;

namespace Ecommerce.Sales.Application.Commands
{
    public class UpdateOrderItemCommand : Command
    {
        public UpdateOrderItemCommand(Guid clientId, Guid productId, int quantity)
        {
            ClientId = clientId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateOrderItemValidation : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliend Id Invalid");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product Id Invalid");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimum item quantity is 1");

            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("Max item quantity is 15");
        }
    }
}
