using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Catalog.Application.Services;
using Ecommerce.Catalog.Domain.Repository;
using Ecommerce.Core.Communication;
using Ecommerce.Sales.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.MVC.Controllers
{
    public class ShopCartController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatrHandler _mediatrHandler;

        public ShopCartController(IProductAppService productAppService, IMediatrHandler mediatrHandler)
        {
            _mediatrHandler = mediatrHandler;
            _productAppService = productAppService;
        }

        [HttpPost]
        [Route("my-cart")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            if (product.StockQuantity < quantity)
            {
                TempData["Erro"] = "Product on stock insuficient";
                return RedirectToAction("ProductDetail", "ShopWindow", new { id });
            }

            var command = new AddOrderItemCommand(ClientId, product.Id, product.Name, quantity, product.Value);

            TempData["Erro"] = "Product unavailable";
            return RedirectToAction("ProductDetail", "ShopWindow", new { id });
        }
    }
}