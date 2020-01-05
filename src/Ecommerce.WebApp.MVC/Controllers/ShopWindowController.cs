using System;
using System.Threading.Tasks;
using Ecommerce.Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.MVC.Controllers
{
    public class ShopWindowController : Controller
    {
        private readonly IProductAppService _productAppService;

        public ShopWindowController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("shop-window")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            return View(await _productAppService.GetById(id));
        }
    }
}