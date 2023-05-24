using Microsoft.EntityFrameworkCore;
using ProductsPricing.Core.Data;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.Repositories
{
    public class ImportRepository : Repository<Import>, IImportRepository
    {
        public ImportRepository(DbContext context) : base(context)
        {
        }

        public override Task<Import?> FindAsync(Guid id)
        {
            return Set
                .Include(x => x.Items)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public IEnumerable<EvaluatedImportItem> GetAllEvaluatedImportItemsWithProductsFromImport(Guid importId)
        {
            return _context.Set<EvaluatedImportItem>()
               .Include(x => x.Product)
               .Where(x => x.ImportId == importId)
               ;
        }

        public IEnumerable<ProcessedImportItem> GetAllProcessedImportItemsWithProductsAndUnitOfMeasureConversionFromImport(Guid importId)
        {
            return _context.Set<ProcessedImportItem>()
                .Include(x => x.Product)
                .ThenInclude(x => x.UnitOfMeasureConversions)
                .Where(x => x.ImportId == importId)
                ;
        }
    }
}