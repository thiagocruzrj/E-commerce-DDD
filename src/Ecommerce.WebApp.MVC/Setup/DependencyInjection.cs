using Ecommerce.Catalog.Application.Services;
using Ecommerce.Catalog.Data;
using Ecommerce.Catalog.Data.Repository;
using Ecommerce.Catalog.Domain.DomainService;
using Ecommerce.Catalog.Domain.Events;
using Ecommerce.Catalog.Domain.Repository;
using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
using Ecommerce.Sales.Application.Commands;
using Ecommerce.Sales.Application.Events;
using Ecommerce.Sales.Application.Queries;
using Ecommerce.Sales.Data;
using Ecommerce.Sales.Data.Repository;
using Ecommerce.Sales.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();

            // Sales
            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

            services.AddScoped<INotificationHandler<OrderDraftStartedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderUpdatedEvent>, OrderEventHandler>();

        }
    }
}
