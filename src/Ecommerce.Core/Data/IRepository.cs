using Ecommerce.Core.DomainObjects;
using System;

namespace Ecommerce.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
