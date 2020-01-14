using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.DomainObjects.DTO;
using Ecommerce.Core.Extensions;
using Ecommerce.Core.Messages;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
using Ecommerce.Sales.Application.Events;
using Ecommerce.Sales.Domain.Entities;
using Ecommerce.Sales.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Application.Commands
{
    public class OrderCommandHandler : 
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<UpdateOrderItemCommand, bool>,
        IRequestHandler<ApplyVoucherOrderItemCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>,
        IRequestHandler<StartOrderCommand, bool>
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
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, message.ProductId);

            if (orderItem != null && !order.OrderItemExist(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order item not found !"));
                return false;
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
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found!"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCode(message.VoucherCode);

            if (voucher == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Voucher not found"));
                return false;
            }

            var voucherApplicationValidation = order.ApplyVoucher(voucher);

            if(!voucherApplicationValidation.IsValid)
            {
                foreach(var error in voucherApplicationValidation.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            order.AddEvent(new OrderUpdatedEvent(message.ClientId, order.Id, order.TotalPrice));
            order.AddEvent(new OrderVoucherAppliedEvent(message.ClientId, order.Id, voucher.Id));

            _orderRepository.UpdateOrder(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(StartOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);
            order.StartOrder();

            var itensList = new List<Item>();
            order.OrderItem.ForEach(i => itensList.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));
            var listOrderProducts = new ListOrderProducts { OrderId = order.Id, Itens = itensList };

            order.AddEvent(new StartedOrderEvent(order.Id, order.ClientId, order.TotalPrice, listOrderProducts, message.CardName, message.CardNumber, message.ExpirateDate, message.CvvCard));

            _orderRepository.UpdateOrder(order);
            return await _orderRepository.UnitOfWork.Commit();
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
