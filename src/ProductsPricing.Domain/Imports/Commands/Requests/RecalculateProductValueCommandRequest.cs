using MediatR;
using ProductsPricing.Domain.Imports.Commands.Responses;

namespace ProductsPricing.Domain.Imports.Commands.Requests
{
    public class RecalculateProductValueCommandRequest : IRequest<RecalculateProductValueCommandResponse>
    {
        public Guid ProductId { get;set; }
    }
}