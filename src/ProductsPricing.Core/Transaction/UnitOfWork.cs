using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductsPricing.Core.Communication;
using ProductsPricing.Core.DomainObjects;

namespace ProductsPricing.Core.Transaction
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IServiceScopeFactory _scopeFactory;

        public UnitOfWork(DbContext context, IServiceScopeFactory scopeFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        public virtual async Task<bool> CommitAsync()
        {
            var hasChanges = await _context.SaveChangesAsync() > 0;

            if (hasChanges)
            {
                var entities = _context.ChangeTracker.Entries<Entity>();
                var entitiesWithEvents = entities.Where(x => x.Entity.Events.Any());
                var events = entitiesWithEvents.SelectMany(x => x.Entity.Events).ToList();

                foreach (var @event in events)
                {
                    await Task.Run(() => EventDispatcher.Dispatch(@event, _scopeFactory));
                }

                foreach (var entity in entities)
                {
                    entity.Entity.ClearEvents();
                }
                
            }

            return hasChanges;
        }

        private async Task PublishEvents()
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();   
            var entities = _context.ChangeTracker.Entries<Entity>();
            var entitiesWithEvents = entities.Where(x => x.Entity.Events.Any());
            var events = entitiesWithEvents.SelectMany(x => x.Entity.Events).ToList();

            entitiesWithEvents.ToList().ForEach(x => x.Entity.ClearEvents());

            var tasks = events.Select(x => mediator.Send(x));
            await Task.WhenAll(tasks);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}