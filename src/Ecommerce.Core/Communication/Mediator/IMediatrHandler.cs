using Ecommerce.Core.Messages;
using System.Threading.Tasks;

namespace Ecommerce.Core.Communication.Mediator
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T eventMediatr) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
    }
}
