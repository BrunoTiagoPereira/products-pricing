using Microsoft.EntityFrameworkCore;
using ProductsPricing.Core.Data;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Ncms.Entities;

namespace ProductsPricing.Data.Repositories
{
    public class NcmRepository : Repository<Ncm>, INcmRepository
    {
        public NcmRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<string> GetExistingNcmCodeValuesFromItems(IEnumerable<string> items)
        {
            return Set
                .Where(x => items.Contains(x.Code.Value))
                .Select(x => x.Code.Value)
                .AsEnumerable()
                ;
        }

        public IEnumerable<NcmProduct> GetNcmProductsFromNcms(IEnumerable<string> ncms)
        {
            return _context
                .Set<NcmProduct>()
                .Include(x => x.Product)
                .Include(x => x.Ncm)
                .Where(x => ncms.Contains(x.Ncm.Code.Value))
                ;
        }
    }
}