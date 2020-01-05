using Ecommerce.Catalog.Application.Services;
using Ecommerce.Catalog.Data;
using Ecommerce.Catalog.Data.Repository;
using Ecommerce.Catalog.Domain.DomainService;
using Ecommerce.Catalog.Domain.Events;
using Ecommerce.Catalog.Domain.Repository;
using Ecommerce.Core.Communication;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
        }
    }
}
