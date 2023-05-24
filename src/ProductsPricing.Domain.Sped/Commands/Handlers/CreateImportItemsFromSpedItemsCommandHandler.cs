using MediatR;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Ncms.Entities;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.Commands.Responses;
using ProductsPricing.Domain.Sped.ValueObjects;
using ProductsPricing.Domain.UnitOfMeasures.Entities;
using ProductsPricing.Domain.Sped.Extensions;
namespace ProductsPricing.Domain.Sped.Commands.Handlers
{
    public class CreateImportItemsFromSpedItemsCommandHandler : IRequestHandler<CreateImportItemsFromSpedItemsCommandRequest, CreateImportItemsFromSpedItemsCommandResponse>
    {
        private readonly IImportRepository _importRepository;
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
        private readonly INcmRepository _ncmRepository;
        private readonly IUnitOfWork _uow;

        public CreateImportItemsFromSpedItemsCommandHandler(IImportRepository importRepository, INcmRepository ncmRepository, IUnitOfWork uow)
        {
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
            _ncmRepository = ncmRepository ?? throw new ArgumentNullException(nameof(ncmRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<CreateImportItemsFromSpedItemsCommandResponse> Handle(CreateImportItemsFromSpedItemsCommandRequest request, CancellationToken cancellationToken)
        {
            var import = await _importRepository.FindAsync(request.ImportId);

            var spedItems = request.Items;

            var spedItemsNcms = spedItems.Select(x => x.Ncm);

            var ncmProducts = _ncmRepository.GetNcmProductsFromNcms(spedItemsNcms);

            var userUnitOfMeasures = _unitOfMeasureRepository.GetAllFromUser(import.UserId);

            CrateImportItemsFromSpedItems(import, spedItems, ncmProducts, userUnitOfMeasures);

            _importRepository.Update(import);
            await _uow.CommitAsync();

            return new CreateImportItemsFromSpedItemsCommandResponse();
        }

        private static void CrateImportItemsFromSpedItems(Import import, IEnumerable<SpedItem> spedItems, IEnumerable<NcmProduct> ncmProducts, IEnumerable<UnitOfMeasure> userUnitOfMeasures)
        {
            foreach (var spedItem in spedItems)
            {
                var pendingProducts = GetPendingProductsFromSpedItem(spedItem, ncmProducts);

                if (HasMoreThanOneProductForSpedItem(pendingProducts))
                {
                    AddImportPendingItem(import, pendingProducts, spedItem.UnitOfMeasureName, spedItem.Value, spedItem.FileLineReference, userUnitOfMeasures);
                }
                else if (HasOneProductForSpedItem(pendingProducts))
                {
                    AddImportProcessedItem(import, spedItem, pendingProducts, userUnitOfMeasures);
                }
            }
        }

        private static IEnumerable<PrimaryProduct> GetPendingProductsFromSpedItem(SpedItem spedItem, IEnumerable<NcmProduct> ncmProducts)
        {
            return ncmProducts
                .Where(x => spedItem.Ncm == x.Ncm.Code.Value)
                .Select(x => x.Product)
                ;
        }

        private static bool HasMoreThanOneProductForSpedItem(IEnumerable<PrimaryProduct> pendingProducts)
        {
            return pendingProducts.Count() > 1;
        }

        private static void AddImportPendingItem(Import import, IEnumerable<PrimaryProduct> pendingProducts, string unitOfMeasureName, decimal newValue, int fileLineReference, IEnumerable<UnitOfMeasure> userUnitOfMeasures)
        {
            import.AddItem(new PendingImportItem(import, pendingProducts, userUnitOfMeasures.FindUnitOfMeasureByName(unitOfMeasureName), newValue, fileLineReference));
        }

        private static bool HasOneProductForSpedItem(IEnumerable<PrimaryProduct> pendingProducts)
        {
            return pendingProducts.Any();
        }

        private static void AddImportProcessedItem(Import import, SpedItem spedItem, IEnumerable<PrimaryProduct> pendingProducts, IEnumerable<UnitOfMeasure> userUnitOfMeasures)
        {
            var product = pendingProducts.First();

            import.AddItem(new ProcessedImportItem(import, product, userUnitOfMeasures.FindUnitOfMeasureByName(spedItem.UnitOfMeasureName), spedItem.Value, spedItem.FileLineReference));
        }
    }
}