using Ecommerce.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SaveEvent<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}
