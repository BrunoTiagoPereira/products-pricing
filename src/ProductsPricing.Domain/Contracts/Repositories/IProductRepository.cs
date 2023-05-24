using ProductsPricing.Core.Data;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Domain.Contracts.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}