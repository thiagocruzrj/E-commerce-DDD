using Ecommerce.Core.DomainObjects.DTO;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Domain.DomainService
{
    public interface IStockService : IDisposable
    {
        Task<bool> DebitStock(Guid productId, int quantity);
        Task<bool> DebitListOrderProducts(ListOrderProducts lista);
        Task<bool> ReplenishStock(Guid productId, int quantity);
        Task<bool> ReplenishListOrderProducts(ListOrderProducts lista);
    }
}
