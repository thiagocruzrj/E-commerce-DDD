using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected Guid ClientId = Guid.Parse("2bc50af4-8dd6-42af-92df-41388829691e");
    }
}
