using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.Messages.CommonMessages.IntegrationEvents;
using Ecommerce.Sales.Application.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<OrderDraftStartedEvent>,
        INotificationHandler<OrderUpdatedEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderStockRejectedEvent>,
        INotificationHandler<PaymentMadeEvent>,
        INotificationHandler<PaymentOrderRefusedEvent>
    {

        private readonly IMediatrHandler _mediatorHandler;

        public OrderEventHandler(IMediatrHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(OrderDraftStartedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderStockRejectedEvent message, CancellationToken cancellationToken)
        {
            // cancel payment order - return error for client
            return Task.CompletedTask;
        }

        public async Task Handle(PaymentMadeEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new FinishOrderCommand(message.OrderId, message.ClientId));
        }

        public async Task Handle(PaymentOrderRefusedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelProcessingOrderReverseStockCommand(message.OrderId, message.ClientId));
        }
    }
}
