using Ecommerce.Core.Data;
using Ecommerce.Sales.Domain.Entities;
using Ecommerce.Sales.Domain.Enums;
using Ecommerce.Sales.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesContext _context;

        public OrderRepository(SalesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Order> GetDraftOrderByClientId(Guid clientId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(p => p.ClientId == clientId && p.OrderStatus == OrderStatus.Draft);
            if (order == null) return null;

            await _context.Entry(order)
                .Collection(i => i.OrderItem).LoadAsync();

            if(order.VoucherId != null)
            {
                await _context.Entry(order)
                    .Reference(i => i.Voucher).LoadAsync();
            }
            return order;
        }

        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(p => p.ProductId == productId && p.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetListByClientId(Guid clientId)
        {
            return await _context.Orders.AsNoTracking().Where(p => p.ClientId == clientId).ToListAsync();
        }

        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == code);
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
        }
    }
}
