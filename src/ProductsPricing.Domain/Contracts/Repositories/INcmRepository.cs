using ProductsPricing.Core.Data;
using ProductsPricing.Domain.Ncms.Entities;

namespace ProductsPricing.Domain.Contracts.Repositories
{
    public interface INcmRepository : IRepository<Ncm>
    {
        IEnumerable<string> GetExistingNcmCodeValuesFromItems(IEnumerable<string> items);

        IEnumerable<NcmProduct> GetNcmProductsFromNcms(IEnumerable<string> ncms);
    }
}