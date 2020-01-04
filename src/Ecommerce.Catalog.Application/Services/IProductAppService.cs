using Ecommerce.Catalog.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetByCategory(int code);
        Task<ProductViewModel> GetById(Guid id);
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<IEnumerable<CategoryViewModel>> GetCategories();

        Task AddProduct(ProductViewModel productViewModel);
        Task UpdateProduct(ProductViewModel productViewModel);

        Task<ProductViewModel> DebitStock(Guid id, int quantity);
        Task<ProductViewModel> ReplenishStock(Guid id, int quantity);
    }
}
