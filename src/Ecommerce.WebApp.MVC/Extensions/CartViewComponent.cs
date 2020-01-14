using Ecommerce.Sales.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IOrderQueries _orderQueries;

        // TODO: Get client logged
        protected Guid ClientId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        public CartViewComponent(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _orderQueries.GetCartByClient(ClientId);
            var items = cart?.Items.Count ?? 0;

            return View(items);
        }
    }
}
