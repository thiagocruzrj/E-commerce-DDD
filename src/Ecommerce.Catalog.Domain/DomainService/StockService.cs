using Ecommerce.Catalog.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Domain.DomainService
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;

        public StockService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> DebitStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            if (!product.HasStock(quantity)) return false;
            product.DebitStock(quantity);
            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplenishStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishStock(quantity);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
