using MediatR;
using ProductsPricing.Core.Extensions;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Imports.Commands.Requests;
using ProductsPricing.Domain.Sped.Commands.Responses;
using ProductsPricing.Domain.Sped.Extensions;
using ProductsPricing.Domain.Sped.ValueObjects;
using System.Text;

namespace ProductsPricing.Domain.Sped.Commands.Requests
{
    public class AddWarningImportLogForSpedItemsWithNoNcmCommandHandler : IRequestHandler<AddWarningImportLogForSpedItemsWithNoNcmCommandRequest, AddWarningImportLogForSpedItemsWithNoNcmCommandResponse>
    {
        public const int ITEM_LOG_DESCRIPTION_MAX_LENGTH = 30;

        private readonly IMediator _mediator;

        public AddWarningImportLogForSpedItemsWithNoNcmCommandHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<AddWarningImportLogForSpedItemsWithNoNcmCommandResponse> Handle(AddWarningImportLogForSpedItemsWithNoNcmCommandRequest request, CancellationToken cancellationToken)
        {
            var sb = new StringBuilder();
            var noNcmItems = request.SpedItems.GetItemsWithNoNcm();

            sb.AppendLine("Alguns itens não serão importados porque não foram encontrados seu NCM no arquivo sped, são eles:");

            foreach (var noNcmItem in noNcmItems)
            {
                string description = GetDescriptionFromSpedItem(noNcmItem);

                sb.AppendLine($"Descrição: {description}, Linha do arquivo: {noNcmItem.FileLineReference}");
            }

            await _mediator.Send(new AddImportLogCommandRequest { ImportId = request.ImportId, LogLevel = LogLevel.Warning(), Message = sb.ToString() });

            return new AddWarningImportLogForSpedItemsWithNoNcmCommandResponse();
        }

        private static string GetDescriptionFromSpedItem(SpedItem spedItem)
        {
            var description = "Sem descrição";

            if (!string.IsNullOrWhiteSpace(spedItem.Description))
            {
                description = spedItem.Description.LimitStringIfExceedsMaxLength(ITEM_LOG_DESCRIPTION_MAX_LENGTH);
            }

            return description;
        }
    }
}