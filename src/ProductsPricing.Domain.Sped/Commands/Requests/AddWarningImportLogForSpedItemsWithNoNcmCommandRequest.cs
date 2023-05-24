using MediatR;
using ProductsPricing.Domain.Sped.Commands.Responses;
using ProductsPricing.Domain.Sped.ValueObjects;

namespace ProductsPricing.Domain.Sped.Commands.Requests
{
    public class AddWarningImportLogForSpedItemsWithNoNcmCommandRequest : IRequest<AddWarningImportLogForSpedItemsWithNoNcmCommandResponse>
    {
        public Guid ImportId { get; set; }

        public IEnumerable<SpedItem> SpedItems { get; set; }
    }
}