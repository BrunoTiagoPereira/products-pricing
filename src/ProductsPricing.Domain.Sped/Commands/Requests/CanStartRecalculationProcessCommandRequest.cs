using MediatR;
using ProductsPricing.Domain.Sped.Commands.Responses;

namespace ProductsPricing.Domain.Sped.Commands.Requests
{
    public class CanStartRecalculationProcessCommandRequest : IRequest<CanStartRecalculationProcessCommandResponse>
    {
        public Guid ImportId { get; set; }
    }
}