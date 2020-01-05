using Ecommerce.Catalog.Application.Services;
using Ecommerce.Catalog.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebApp.MVC.Controllers.Admin
{
    public class AdminProductsController : Controller
    {
        private readonly IProductAppService _productAppService;

        public AdminProductsController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("admin-products")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [Route("new-product")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await PopulateCategories(new ProductViewModel()));
        }

        [Route("new-product")]
        [HttpPost]
        public async Task<IActionResult> NewProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(await PopulateCategories(productViewModel));
            await _productAppService.AddProduct(productViewModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("new-product")]
        public async Task<ProductViewModel> PopulateCategories(ProductViewModel product)
        {
            product.Categories = await _productAppService.GetCategories();
            return product;
        }

        [HttpGet]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return View(await PopulateCategories(await _productAppService.GetById(id)));
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel productViewModel)
        {
            var product = await _productAppService.GetById(id);
            productViewModel.StockQuantity = product.StockQuantity;

            ModelState.Remove("StockQuantity");
            if (!ModelState.IsValid) return View(await PopulateCategories(productViewModel));

            await _productAppService.UpdateProduct(productViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id)
        {
            return View("Stock", await _productAppService.GetById(id));
        }

        [HttpPost]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id, int quantity)
        {
            if (quantity > 0)
            {
                await _productAppService.ReplenishStock(id, quantity);
            } else
            {
                await _productAppService.DebitStock(id, quantity);
            }
            return View("Index", await _productAppService.GetAll());
        }
    }
}
