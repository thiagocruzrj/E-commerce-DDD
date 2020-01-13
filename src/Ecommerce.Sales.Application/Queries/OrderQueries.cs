using Ecommerce.Sales.Application.Queries.ViewModels;
using Ecommerce.Sales.Domain.Enums;
using Ecommerce.Sales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CartViewModel> GetCartByClient(Guid clientId)
        {
            var order = await _orderRepository.GetDraftOrderByClientId(clientId);
            if (order == null) return null;

            var cart = new CartViewModel
            {
                ClientId = order.ClientId,
                TotalPrice = order.TotalPrice,
                OrderId = order.Id,
                DescountValue = order.Discount,
                SubTotal = order.Discount + order.TotalPrice
            };

            if (order.VoucherId != null)
            {
                cart.VoucherCode = order.Voucher.Code;
            }

            foreach (var item in order.OrderItem)
            {
                cart.Items.Add(new CartItemViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitValue = item.UnitValue,
                    TotalPrice = item.UnitValue * item.Quantity
                });
            }

            return cart;
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersByClient(Guid clientId)
        {
            var orders = await _orderRepository.GetListByClientId(clientId);

            orders = orders.Where(p => p.OrderStatus == OrderStatus.Paid || p.OrderStatus == OrderStatus.Canceled)
                .OrderByDescending(p => p.Code);

            if (!orders.Any()) return null;

            var orderView = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                orderView.Add(new OrderViewModel
                {
                    TotalPrice = order.TotalPrice,
                    OrderStatus = (int)order.OrderStatus,
                    Code = order.Code,
                    RegisterDate = order.RegisterDate
                });
            }

            return orderView;
        }
    }
}
