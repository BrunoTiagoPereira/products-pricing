using MediatR;
using ProductsPricing.Domain.Imports.Commands.Responses;

namespace ProductsPricing.Domain.Imports.Commands.Requests
{
    public class CreateEvaluatedItemsFromProcessedItemsCommandRequest : IRequest<CreateEvaluatedItemsFromProcessedItemsCommandResponse>
    {
        public Guid ImportId { get; set; }
    }
}