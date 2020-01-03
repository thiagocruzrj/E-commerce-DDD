using Ecommerce.Catalog.Domain.Events;
using Ecommerce.Catalog.Domain.Repository;
using Ecommerce.Core.Communication;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Domain.DomainService
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatrHandler _mediatorHandler;

        public StockService(IProductRepository productRepository, IMediatrHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> DebitStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            if (!product.HasStock(quantity)) return false;
            product.DebitStock(quantity);

            // TODO: Set the low stock quantity
            if (product.StockQuantity < 10)
                await _mediatorHandler.PublishEvent(new ProductBelowStockEvent(product.Id, product.StockQuantity));

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
