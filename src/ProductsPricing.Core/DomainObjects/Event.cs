using MediatR;

namespace ProductsPricing.Core.DomainObjects
{
    public abstract class Event : INotification
    {
        public Guid AggregateRootId { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}