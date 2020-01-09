﻿using Ecommerce.Core.Messages;
using Ecommerce.Sales.Domain.Entities;
using Ecommerce.Sales.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
            }
            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;
            foreach (var error in message.ValidationResult.Errors)
            {
                // launching an error event
            }

            return false;
        }
    }
}