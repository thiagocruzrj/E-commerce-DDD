using Ecommerce.Core.Messages;
using FluentValidation;
using System;

namespace Ecommerce.Sales.Application.Commands
{
    public class ApplyVoucherOrderItemCommand : Command
    {
        public ApplyVoucherOrderItemCommand(Guid clientId, Guid orderId, string voucherCode)
        {
            ClientId = clientId;
            OrderId = orderId;
            VoucherCode = voucherCode;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public string VoucherCode{ get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new ApplyVoucherOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ApplyVoucherOrderItemValidation : AbstractValidator<ApplyVoucherOrderItemCommand>
    {
        public ApplyVoucherOrderItemValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliend Id Invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order Id Invalid");
        }
    }
}
