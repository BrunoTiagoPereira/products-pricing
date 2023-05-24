using MediatR;
using ProductsPricing.Domain.Sintegra.Commands.Requests;
using ProductsPricing.Domain.Sintegra.Commands.Responses;

namespace ProductsPricing.Domain.Sintegra.Commands.Handlers
{
    public class CreateSintegraImportCommandHandler : IRequestHandler<CreateSintegraProductsFromItemsCommandRequest, CreateSintegraProductsFromItemsCommandResponse>
    {

        public async Task<CreateSintegraProductsFromItemsCommandResponse> Handle(CreateSintegraProductsFromItemsCommandRequest request, CancellationToken cancellationToken)
        {

            return new CreateSintegraProductsFromItemsCommandResponse();
        }
    }
}
