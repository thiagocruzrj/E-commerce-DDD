using Ecommerce.Core.Communication.Mediator;
using Ecommerce.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Sales.Data.MediatRConfig
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents(this IMediatrHandler mediator, SalesContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.CleanEvents());

            var tasks = domainEvents
                .Select(async (domainEvents) =>
                {
                    await mediator.PublishEvent(domainEvents);
                }).ToList();

            await Task.WhenAll(tasks);
        }
    }
}
