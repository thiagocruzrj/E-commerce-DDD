using Ecommerce.Core.DomainObjects;
using Ecommerce.Sales.Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Ecommerce.Sales.Domain.Entities
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public VoucherDiscountType VoucherDiscountType { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? DateUse { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        // EF Rel.
        public ICollection<Order> Orders { get; set; }

        internal ValidationResult ValidadeIfApplicable()
        {
            return new VoucherApplicableValidation().Validate(this);
        }
    }

    public class VoucherApplicableValidation : AbstractValidator<Voucher>
    {
        public VoucherApplicableValidation()
        {
            RuleFor(c => c.DueDate)
                .Must(DueDateGreaterThanCurrent)
                .WithMessage("Voucher expired !");

            RuleFor(c => c.Active)
                .Equal(true)
                .WithMessage("Voucher not valid !");

            RuleFor(c => c.Used)
                .Equal(false)
                .WithMessage("Voucher user !");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Voucher not available !");
        }

        protected static bool DueDateGreaterThanCurrent(DateTime dueDate)
        {
            return dueDate >= DateTime.Now;
        }
    }
}
