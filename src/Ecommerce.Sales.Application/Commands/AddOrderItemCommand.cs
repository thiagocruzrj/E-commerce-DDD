using Ecommerce.Core.Messages;
using FluentValidation;
using System;

namespace Ecommerce.Sales.Application.Commands
{
    public class AddOrderItemCommand : Command
    {
        public AddOrderItemCommand(Guid clientId, Guid productId, string name, int quantity, decimal unitValue)
        {
            ClientId = clientId;
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Client Id Invalid");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product Id Invalid");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Client Id Invalid");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimum item quantity is 1");

            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("Max item quantity is 15");

            RuleFor(c => c.UnitValue)
                 .GreaterThan(0)
                 .WithMessage("Item value must be greater than 0");
        }
    }
}
