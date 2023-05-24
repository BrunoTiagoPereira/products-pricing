using Microsoft.EntityFrameworkCore;
using ProductsPricing.Core.DomainObjects;
using System.Linq.Expressions;

namespace ProductsPricing.Core.Data
{
    public interface IRepository<T> : IDisposable where T : class, IAggregateRoot
    {
        public DbSet<T> Set { get; }

        Task<T?> FindAsync(Guid id);

        Task<T?> FirstOrDefautAsync(Expression<Func<T, bool>> predicate);

        Task<T?> SingleOrDefautAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        void Update(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
        
    }
}