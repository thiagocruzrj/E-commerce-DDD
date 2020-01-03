using Ecommerce.Core.Messages;
using System.Threading.Tasks;

namespace Ecommerce.Core.Communication
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T eventMediatr) where T : Event;
    }
}
