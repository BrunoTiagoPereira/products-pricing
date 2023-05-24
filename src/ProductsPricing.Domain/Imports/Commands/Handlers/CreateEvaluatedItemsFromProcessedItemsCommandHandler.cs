using MediatR;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Commands.Requests;
using ProductsPricing.Domain.Imports.Commands.Responses;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Domain.Imports.Commands.Handlers
{
    public class CreateEvaluatedItemsFromProcessedItemsCommandHandler : IRequestHandler<CreateEvaluatedItemsFromProcessedItemsCommandRequest, CreateEvaluatedItemsFromProcessedItemsCommandResponse>
    {
        private readonly IImportRepository _importRepository;
        private readonly IUnitOfWork _uow;

        public CreateEvaluatedItemsFromProcessedItemsCommandHandler(IImportRepository importRepository, IUnitOfWork uow)
        {
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<CreateEvaluatedItemsFromProcessedItemsCommandResponse> Handle(CreateEvaluatedItemsFromProcessedItemsCommandRequest request, CancellationToken cancellationToken)
        {
            var import = await _importRepository.FindAsync(request.ImportId);

            var processedImportItems = _importRepository.GetAllProcessedImportItemsWithProductsAndUnitOfMeasureConversionFromImport(request.ImportId);

            var productProcessedImportItemsGroup = processedImportItems.GroupBy(x => x.Product);

            foreach (var item in productProcessedImportItemsGroup)
            {
                var productNewValue = GetAverageValueFromProcessedImportItems(item);

                AddEvaluatedImportItemToImport(import, item.Key, productNewValue);
            }

            _importRepository.Update(import);
            await _uow.CommitAsync();

            return new CreateEvaluatedItemsFromProcessedItemsCommandResponse { };
        }

        private static void AddEvaluatedImportItemToImport(Import import, PrimaryProduct primaryProduct, decimal productNewValue)
        {
            import.AddItem(new EvaluatedImportItem(import, primaryProduct, productNewValue));
        }

        private static decimal GetAverageValueFromProcessedImportItems(IGrouping<PrimaryProduct, ProcessedImportItem> processedImportItems)
        {
            decimal processedImportItemSum = 0;

            foreach (var processedImportItem in processedImportItems)
            {
                var conversion = GetConversionFromProduct(processedImportItem);

                processedImportItemSum += conversion.Convert(processedImportItem.NewValue);
            }
            return processedImportItemSum / processedImportItems.Count();
        }

        private static UnitOfMeasureConversion GetConversionFromProduct(ProcessedImportItem processedImportItem)
        {
            return processedImportItem
                .Product
                .UnitOfMeasureConversions
                .First(x => x.UnitOfMeasure == processedImportItem.UnitOfMeasure)
                ;
        }
    }
}