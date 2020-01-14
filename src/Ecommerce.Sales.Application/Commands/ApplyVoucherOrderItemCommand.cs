using Ecommerce.Core.Messages;
using FluentValidation;
using System;

namespace Ecommerce.Sales.Application.Commands
{
    public class ApplyVoucherOrderItemCommand : Command
    {
        public ApplyVoucherOrderItemCommand(Guid clientId, string voucherCode)
        {
            ClientId = clientId;
            VoucherCode = voucherCode;
        }

        public Guid ClientId { get; private set; }
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

            RuleFor(c => c.VoucherCode)
                .NotEmpty()
                .WithMessage("Voucher code couldn't Invalid");
        }
    }
}
