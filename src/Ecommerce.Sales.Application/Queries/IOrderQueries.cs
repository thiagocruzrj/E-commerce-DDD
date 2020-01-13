using Ecommerce.Sales.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Application.Queries
{
    public interface IOrderQueries
    {
        Task<CartViewModel> GetCartByClient(Guid clientId);
        Task<IEnumerable<OrderViewModel>> GetOrdersByClient(Guid clientId);
    }
}
