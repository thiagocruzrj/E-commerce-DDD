using System;
using System.Threading.Tasks;
using Ecommerce.Catalog.Application.Services;
using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
using Ecommerce.Sales.Application.Commands;
using Ecommerce.Sales.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.MVC.Controllers
{
    public class ShopCartController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IOrderQueries _orderQueries;
        private readonly IMediatrHandler _mediatrHandler;

        public ShopCartController(INotificationHandler<DomainNotification> notifications,
                                    IProductAppService productAppService,
                                    IOrderQueries orderQueries,
                                    IMediatrHandler mediatrHandler)
                                    : base(notifications, mediatrHandler)
        {
            _mediatrHandler = mediatrHandler;
            _orderQueries = orderQueries;
            _productAppService = productAppService;
        }

        [Route("my-cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderQueries.GetCartByClient(ClientId));
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
            await _mediatrHandler.SendCommand(command);

            if(ValidOperation())
            {
                return RedirectToAction("Index");
            }

            TempData["Erros"] = GetErroMessage();
            return RedirectToAction("ProductDetail", "ShopWindow", new { id });
        }

        //[HttpPost]
        //[Route("remove-item")]
        //public async Task<IActionResult> RemoveItem(Guid id)
        //{
        //    var product = await _productAppService.GetById(id);
        //    if (product == null) return BadRequest();

        //    var command = new RemoveOrderItemCommand(ClientId, id);
        //    await _mediatrHandler.SendCommand(command);

        //    if(ValidOperation())
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", await _orderQueries.GetCartByClient(ClientId));
        //}

        //[HttpPost]
        //[Route("update-item")]
        //public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        //{
        //    var product = await _productAppService.GetById(id);
        //    if (product == null) return BadRequest();

        //    var command = new UpdateOrderItemCommand(ClientId, id, quantity);
        //    await _mediatrHandler.SendCommand(command);

        //    if(ValidOperation())
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        //[HttpPost]
        //[Route("apply-voucher")]
        //public async Task<IActionResult> ApplyVoucher(string voucherCode)
        //{
        //    var command = new ApplyVoucherOrderItemCommand(ClientId, voucherCode);
        //    await _mediatrHandler.SendCommand(command);

        //    if(ValidOperation())
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", await _orderQueries.GetCartByClient(ClientId));
        //}
    }
}