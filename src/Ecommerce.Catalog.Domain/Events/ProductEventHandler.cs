using Ecommerce.Catalog.Domain.DomainService;
using Ecommerce.Catalog.Domain.Repository;
using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.Messages.CommonMessages.IntegrationEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductBelowStockEvent>,
                                       INotificationHandler<StartedOrderEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMediatrHandler _mediatorHandler;

        public ProductEventHandler(IProductRepository productRepository, IStockService stockService, IMediatrHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(ProductBelowStockEvent message, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(message.AggregateId);
            // For example send an email to get more products
        }

        public async Task Handle(StartedOrderEvent message, CancellationToken cancellationToken)
        {
            var result = await _stockService.DebitListOrderProducts(message.OrderProducts);

            if (result)
            {
                await _mediatorHandler.PublishEvent(new OrderStockConfirmed(message.OrderId, message.ClientId, message.Total, message.OrderProducts, message.CardName, message.CardNumber, message.ExpirationDate, message.CvvCard ));
            } else
            {
                await _mediatorHandler.PublishEvent(new OrderStockRejectedEvent(message.OrderId, message.ClientId));
            }
        }
    }
}
