using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.DomainObjects.DTO;
using Ecommerce.Core.Messages.CommonMessages.IntegrationEvents;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
using Ecommerce.Payments.Business.Entities;
using Ecommerce.Payments.Business.Enums;
using Ecommerce.Payments.Business.Repository;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Payments.Business.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentCreditCardFacade _paymentCreditCardFacade;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediatrHandler _mediatorHandler;

        public PaymentService(IPaymentCreditCardFacade paymentCreditCardFacade, IPaymentRepository paymentRepository, IMediatrHandler mediatorHandler)
        {
            _paymentCreditCardFacade = paymentCreditCardFacade;
            _paymentRepository = paymentRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transaction> MakeOrderPayment(PaymentOrder paymentOrder)
        {
            var order = new Order
            {
                Id = paymentOrder.OrderId,
                Value = paymentOrder.Total
            };

            var payment = new Payment
            {
                Value = paymentOrder.Total,
                CardName = paymentOrder.CardName,
                CardNumber = paymentOrder.CardNumber,
                ExpirationCard = paymentOrder.ExpirationCard,
                CvvCard = paymentOrder.CvvCard,
                OrderId = paymentOrder.OrderId
            };

            var transaction = _paymentCreditCardFacade.MakePayment(order, payment);

            if(transaction.StatusTransaction == StatusTransaction.Paid)
            {
                payment.AddEvent(new PaymentMadeEvent(order.Id, paymentOrder.ClientId, transaction.PaymentId, transaction.Id, order.Value));

                _paymentRepository.Add(payment);
                _paymentRepository.AddTransaction(transaction);

                await _paymentRepository.UnitOfWork.Commit();
                return transaction;
            }

            await _mediatorHandler.PublishNotification(new DomainNotification("payment", "Card operator refused the payment"));
            await _mediatorHandler.PublishEvent(new PaymentOrderRefusedEvent(order.Id, paymentOrder.ClientId, transaction.PaymentId, transaction.Id, order.Value));

            return transaction;
        }
    }
}
