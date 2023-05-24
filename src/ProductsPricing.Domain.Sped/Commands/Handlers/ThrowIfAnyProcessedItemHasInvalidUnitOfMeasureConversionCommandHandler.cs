using MediatR;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Core.Extensions;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.Commands.Responses;
using System.Text;

namespace ProductsPricing.Domain.Sped.Commands.Handlers
{
    public class ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandHandler : IRequestHandler<ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandRequest, ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandResponse>
    {
        private readonly IImportRepository _importRepository;

        public ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandHandler(IImportRepository importRepository)
        {
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
        }

        public async Task<ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandResponse> Handle(ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandRequest request, CancellationToken cancellationToken)
        {
            var importProcessedImportItems = _importRepository.GetAllProcessedImportItemsWithProductsAndUnitOfMeasureConversionFromImport(request.ImportId);

            var invalidProcessedImportItems = new List<ProcessedImportItem>();

            foreach (var processedImportItem in importProcessedImportItems)
            {
                if (ProductDoesNotHaveConversionForProcessedItem(processedImportItem))
                {
                    invalidProcessedImportItems.Add(processedImportItem);
                }
            }

            if (invalidProcessedImportItems.Any())
            {
                var invalidProcessedErrorMessage = GetInvalidProcessedItemsErrorMessage(invalidProcessedImportItems);

                throw new DomainException($"Há itens inválidos na importação pois não tem cadastrados a conversão da unidade de medida no produto. Itens inválidos: {invalidProcessedErrorMessage}");
            }

            return new ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandResponse();
        }

        private static bool ProductDoesNotHaveConversionForProcessedItem(ProcessedImportItem processedImportItem)
        {
            return !processedImportItem.Product.UnitOfMeasureConversions.Any(x => x.UnitOfMeasure == processedImportItem.UnitOfMeasure);
        }

        private string GetInvalidProcessedItemsErrorMessage(List<ProcessedImportItem> invalidProcessedImportItems)
        {
            StringBuilder sb = new();

            foreach (var invalidProcessedImportItem in invalidProcessedImportItems)
            {
                sb.AppendLine($"Linha do arquivo: {invalidProcessedImportItem.FileLineReference}, Unidade de medida: {invalidProcessedImportItem.UnitOfMeasure.Name}, Produto: {invalidProcessedImportItem.Product.Name.LimitStringIfExceedsMaxLength(20)}");
            }

            return sb.ToString();
        }
    }
}