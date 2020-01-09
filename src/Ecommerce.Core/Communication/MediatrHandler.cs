using Ecommerce.Core.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Communication
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
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
