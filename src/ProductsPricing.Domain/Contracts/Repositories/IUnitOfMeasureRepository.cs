using ProductsPricing.Core.Data;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Domain.Contracts.Repositories
{
    public interface IUnitOfMeasureRepository : IRepository<UnitOfMeasure>
    {
        IEnumerable<UnitOfMeasure> GetAllFromUser(Guid userId);
    }
}