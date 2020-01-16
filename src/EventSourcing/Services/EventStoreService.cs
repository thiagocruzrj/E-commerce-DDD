using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcing.Services
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreService(IEventStoreConnection connection)
        {
            _connection = EventStoreConnection.Create("");
            _connection.ConnectAsync();
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;
        }
    }
}
