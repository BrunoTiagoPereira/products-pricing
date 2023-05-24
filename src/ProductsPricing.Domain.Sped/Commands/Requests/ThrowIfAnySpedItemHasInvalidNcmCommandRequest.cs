using MediatR;
using ProductsPricing.Domain.Sped.Commands.Responses;
using ProductsPricing.Domain.Sped.ValueObjects;

namespace ProductsPricing.Domain.Sped.Commands.Requests
{
    public class ThrowIfAnySpedItemHasInvalidNcmCommandRequest : IRequest<ThrowIfAnySpedItemHasInvalidNcmCommandResponse>
    {
        public IEnumerable<SpedItem> Items { get; set; }
    }
}