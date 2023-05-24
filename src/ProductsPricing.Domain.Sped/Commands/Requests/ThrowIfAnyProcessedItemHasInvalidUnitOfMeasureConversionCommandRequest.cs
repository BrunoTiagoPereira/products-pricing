using MediatR;
using ProductsPricing.Domain.Sped.Commands.Responses;

namespace ProductsPricing.Domain.Sped.Commands.Requests
{
    public class ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandRequest : IRequest<ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandResponse>
    {
        public Guid ImportId { get; set; }
    }
}