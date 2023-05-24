using MediatR;
using ProductsPricing.Domain.Sped.Commands.Responses;
using ProductsPricing.Domain.Sped.ValueObjects;

namespace ProductsPricing.Domain.Sped.Commands.Requests
{
    public class CreateImportItemsFromSpedItemsCommandRequest : IRequest<CreateImportItemsFromSpedItemsCommandResponse>
    {
        public Guid ImportId { get; set; }
        public IEnumerable<SpedItem> Items { get; set; }
    }
}