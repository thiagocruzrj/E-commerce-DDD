using Ecommerce.Core.Messages;
using FluentValidation;
using System;

namespace Ecommerce.Sales.Application.Commands
{
    public class StartOrderCommand : Command
    {
        public StartOrderCommand(Guid orderId, Guid clientId, decimal total, string cardName, string cardNumber, string expirateDate, string cvvCard)
        {
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            ExpirateDate = expirateDate;
            CvvCard = cvvCard;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpirateDate { get; private set; }
        public string CvvCard { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class StartOrderValidation : AbstractValidator<StartOrderCommand>
    {
        public StartOrderValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Client Id Invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order Id Invalid");

            RuleFor(c => c.CardName)
                .NotEmpty()
                .WithMessage("Card name not filled");

            RuleFor(c => c.CardNumber)
                .CreditCard()
                .WithMessage("Card number invalid");

            RuleFor(c => c.ExpirateDate)
                .NotEmpty()
                .WithMessage("Expiration Date not filled");

            RuleFor(c => c.CvvCard)
                 .Length(3,4)
                 .WithMessage("CVV not filled correctly");
        }
    }
}
