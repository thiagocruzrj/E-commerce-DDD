using Ecommerce.Catalog.Domain.Events;
using Ecommerce.Catalog.Domain.Repository;
using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.DomainObjects.DTO;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
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
            if (!await DebitStockItem(productId, quantity)) return false;

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DebitListOrderProducts(ListOrderProducts list)
        {
            foreach (var item in list.Itens)
            {
                if (!await DebitStockItem(item.Id, item.Quantity)) return false;
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitStockItem(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null) return false;

            if (!product.HasStock(quantity))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Stock", $"Product - {product.Name} out of stock."));
                return false;
            }

            product.DebitStock(quantity);

            // TODO: 10 can parameterizable in config file
            if (product.StockQuantity < 10)
            {
                await _mediatorHandler.PublishEvent(new ProductBelowStockEvent(product.Id, product.StockQuantity));
            }

            _productRepository.Update(product);
            return true;
        }

        public async Task<bool> ReplenishStock(Guid productId, int quantity)
        {
            var success = await ReplenishItemStock(productId, quantity);

            if (!success) return false;

            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> ReplenishItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishStock(quantity);

            _productRepository.Update(product);

            return true;
        }

        public async Task<bool> ReplenishListOrderProducts(ListOrderProducts list)
        {
            foreach (var item in list.Itens)
            {
                await ReplenishItemStock(item.Id, item.Quantity);
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
