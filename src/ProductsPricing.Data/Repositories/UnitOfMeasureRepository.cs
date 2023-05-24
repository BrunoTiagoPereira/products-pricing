using Microsoft.EntityFrameworkCore;
using ProductsPricing.Core.Data;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Data.Repositories
{
    public class UnitOfMeasureRepository : Repository<UnitOfMeasure>, IUnitOfMeasureRepository
    {
        public UnitOfMeasureRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<UnitOfMeasure> GetAllFromUser(Guid userId)
        {
            return Set.Where(x => x.UserId == userId);
        }
    }
}