using Ecommerce.Core.Messages;
using Ecommerce.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Ecommerce.Core.Communication.Mediator
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T eventMediatr) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PubishNotification<T>(T notification) where T : DomainNotification;
    }
}
