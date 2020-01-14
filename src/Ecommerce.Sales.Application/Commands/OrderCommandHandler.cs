using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.Messages;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
using Ecommerce.Sales.Application.Events;
using Ecommerce.Sales.Domain.Entities;
using Ecommerce.Sales.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Application.Commands
{
    public class OrderCommandHandler : 
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<UpdateOrderItemCommand, bool>,
        IRequestHandler<ApplyVoucherOrderItemCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatrHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatrHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);
            var orderItem = new OrderItem(message.ProductId, message.Name, message.Quantity, message.UnitValue);

            if(order == null)
            {
                order = Order.OrderFactory.NewOrderDraft(message.ClientId);
                order.AddItem(orderItem);

                _orderRepository.AddOrder(order);
                order.AddEvent(new OrderDraftStartedEvent(message.ClientId, message.ProductId));
            } else
            {
                var orderItemExistent = order.OrderItemExist(orderItem);
                order.AddItem(orderItem);

                if (orderItemExistent)
                {
                    _orderRepository.UpdateOrderItem(order.OrderItem.FirstOrDefault(p => p.ProductId == orderItem.ProductId));
                } else
                {
                    _orderRepository.AddOrderItem(orderItem);
                }

                order.AddEvent(new OrderUpdatedEvent(order.ClientId, order.Id, order.TotalPrice));
            }
            order.AddEvent(new OrderItemAddedEvent(order.ClientId, order.Id,message.ProductId,message.Name, message.UnitValue, message.Quantity));
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found !"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, message.ProductId);

            if (!order.OrderItemExist(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order item not found !"));
                return false;
            }

            order.UpdateUnits(orderItem, message.Quantity);

            order.AddEvent(new OrderUpdatedEvent(order.ClientId, order.Id, order.TotalPrice));
            order.AddEvent(new OrderProductUpdatedEvent(message.ClientId, order.Id, message.ProductId, message.Quantity));

            _orderRepository.UpdateOrderItem(orderItem);
            _orderRepository.UpdateOrder(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found!"));
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, message.ProductId);

            if (orderItem != null && !order.OrderItemExist(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order item not found !"));
            }

            order.RemoveItem(orderItem);

            order.AddEvent(new OrderUpdatedEvent(message.ClientId, order.Id, order.TotalPrice));
            order.AddEvent(new OrderProductRemovedEvent(message.ClientId, order.Id, message.ProductId));

            _orderRepository.RemoveOrderItem(orderItem);
            _orderRepository.UpdateOrder(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ApplyVoucherOrderItemCommand message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;
            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
