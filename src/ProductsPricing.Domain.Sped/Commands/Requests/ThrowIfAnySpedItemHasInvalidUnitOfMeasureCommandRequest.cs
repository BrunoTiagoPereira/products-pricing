using MediatR;
using ProductsPricing.Domain.Sped.Commands.Responses;
using ProductsPricing.Domain.Sped.ValueObjects;

namespace ProductsPricing.Domain.Sped.Commands.Requests
{
    public class ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandRequest : IRequest<ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandResponse>
    {
        public Guid UserId { get; set; }
        public IEnumerable<SpedItem> Items { get; set; }
    }
}