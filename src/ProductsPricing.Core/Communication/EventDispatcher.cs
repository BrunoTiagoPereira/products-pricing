using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ProductsPricing.Core.Communication
{
    public static class EventDispatcher
    {
        public static void Dispatch(INotification notification, IServiceScopeFactory scopeFactory)
        {
            using var scope = scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            mediator.Publish(notification).GetAwaiter().GetResult();
        }
    }
}