using MediatR;
using ProductsPricing.Domain.Imports.Commands.Responses;

namespace ProductsPricing.Domain.Imports.Commands.Requests
{
    public class CreateRecalculationJobForImportEvaluatedItemsCommandRequest : IRequest<CreateRecalculationJobForImportEvaluatedItemsCommandResponse>
    {
        public Guid ImportId { get; set; }
    }
}