using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatrHandler _mediator;

        protected ControllerBase(INotificationHandler<DomainNotification> notifications, IMediatrHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected bool ValidOperation()
        {
            return !_notifications.HasNotification();
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.PublishNotification(new DomainNotification(code, message));
        }

        protected Guid ClientId = Guid.Parse("2bc50af4-8dd6-42af-92df-41388829691e");
    }
}
