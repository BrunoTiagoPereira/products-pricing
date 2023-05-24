using MediatR;
using ProductsPricing.Domain.Imports.Commands.Responses;

namespace ProductsPricing.Domain.Imports.Commands.Requests
{
    public class UpdateProductValuesFromEvaluatedItemsCommandRequest : IRequest<UpdateProductValuesFromEvaluatedItemsCommandResponse>
    {
        public Guid ImportId { get; set; }
    }
}