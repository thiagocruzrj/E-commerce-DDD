using EventStore.ClientAPI;

namespace EventSourcing.Services
{
    public interface IEventStoreService
    {
        IEventStoreConnection GetConnection();
    }
}
