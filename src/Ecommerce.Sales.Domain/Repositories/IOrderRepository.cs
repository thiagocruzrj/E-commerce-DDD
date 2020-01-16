using Ecommerce.Core.Data;
using Ecommerce.Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetByOrderId(Guid id);
        Task<IEnumerable<Order>> GetListByClientId(Guid clientId);
        Task<Order> GetDraftOrderByClientId(Guid clientId);
        void AddOrder(Order order);
        void UpdateOrder(Order order);

        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
        void AddOrderItem(OrderItem orderItem);
        void UpdateOrderItem(OrderItem orderItem);
        void RemoveOrderItem(OrderItem orderItem);

        Task<Voucher> GetVoucherByCode(string code);
    }
}
