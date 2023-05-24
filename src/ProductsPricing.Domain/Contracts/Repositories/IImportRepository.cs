using ProductsPricing.Core.Data;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Domain.Contracts.Repositories
{
    public interface IImportRepository : IRepository<Import>
    {
        IEnumerable<ProcessedImportItem> GetAllProcessedImportItemsWithProductsAndUnitOfMeasureConversionFromImport(Guid importId);

        IEnumerable<EvaluatedImportItem> GetAllEvaluatedImportItemsWithProductsFromImport(Guid importId);
    }
}