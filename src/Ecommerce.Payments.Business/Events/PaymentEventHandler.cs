using Ecommerce.Core.DomainObjects.DTO;
using Ecommerce.Core.Messages.CommonMessages.IntegrationEvents;
using Ecommerce.Payments.Business.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Payments.Business.Events
{
    public class PaymentEventHandler : INotificationHandler<OrderStockConfirmedEvent>
    {
        private readonly IPaymentService _paymentService;

        public PaymentEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(OrderStockConfirmedEvent message, CancellationToken cancellationToken)
        {
            var paymentOrder = new PaymentOrder
            {
                OrderId = message.OrderId,
                ClientId = message.ClientId,
                Total = message.Total,
                CardName = message.CardName,
                CardNumber = message.CardNumber,
                ExpirationCard = message.ExpirationCard,
                CvvCard = message.CvvCard
            };

            await _paymentService.MakeOrderPayment(paymentOrder);
        }
    }
}
