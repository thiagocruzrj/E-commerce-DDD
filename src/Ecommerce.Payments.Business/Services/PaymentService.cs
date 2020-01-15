using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.DomainObjects.DTO;
using Ecommerce.Payments.Business.Entities;
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

        public Task<Transaction> MakeOrderPayment(PaymentOrder paymentOrder)
        {
            throw new NotImplementedException();
        }
    }
}
