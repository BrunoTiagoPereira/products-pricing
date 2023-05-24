using MediatR;
using ProductsPricing.Domain.Sintegra.Commands.Responses;
using ProductsPricing.Domain.Sintegra.Dtos;

namespace ProductsPricing.Domain.Sintegra.Commands.Requests
{
    public class CreateSintegraProductsFromItemsCommandRequest : IRequest<CreateSintegraProductsFromItemsCommandResponse>
    {
        public Guid ImportId { get; set; }
        public List<SintegraItem> Items { get; set; }
    }
}
