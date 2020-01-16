using Ecommerce.Catalog.Application.Services;
using Ecommerce.Catalog.Data;
using Ecommerce.Catalog.Data.Repository;
using Ecommerce.Catalog.Domain.DomainService;
using Ecommerce.Catalog.Domain.Events;
using Ecommerce.Catalog.Domain.Repository;
using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.Messages.CommonMessages.IntegrationEvents;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
using Ecommerce.Payments.AntiCorruption;
using Ecommerce.Payments.AntiCorruption.Config;
using Ecommerce.Payments.AntiCorruption.Gateway;
using Ecommerce.Payments.Business;
using Ecommerce.Payments.Business.Events;
using Ecommerce.Payments.Business.Repository;
using Ecommerce.Payments.Business.Services;
using Ecommerce.Payments.Data;
using Ecommerce.Sales.Application.Commands;
using Ecommerce.Sales.Application.Events;
using Ecommerce.Sales.Application.Queries;
using Ecommerce.Sales.Data;
using Ecommerce.Sales.Data.Repository;
using Ecommerce.Sales.Domain.Repositories;
using EventSourcing.Services;
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

            // Event Sourcing
            services.AddSingleton<IEventStoreService,EventStoreService>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<StartedOrderEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<ProcessOrderCanceledEvent>, ProductEventHandler>();

            // Sales
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyVoucherOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<StartOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<FinishOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelProcessingOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelProcessingOrderReverseStockCommand, bool>, OrderCommandHandler>();

            services.AddScoped<INotificationHandler<OrderDraftStartedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderStockRejectedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<PaymentMadeEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<PaymentOrderRefusedEvent>, OrderEventHandler>();

            // Payment
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentCreditCardFacade, PaymentCreditCardFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<PaymentContext>();

            services.AddScoped<INotificationHandler<OrderStockConfirmedEvent>, PaymentEventHandler>();
        }
    }
}
