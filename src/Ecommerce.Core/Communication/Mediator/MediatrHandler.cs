using Ecommerce.Core.Messages;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
using MediatR;
using System.Threading.Tasks;

namespace Ecommerce.Core.Communication.Mediator
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PubishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }

        public async Task PublishEvent<T>(T eventMediatr) where T : Event
        {
            await _mediator.Publish(eventMediatr);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
