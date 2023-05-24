using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Core.Exceptions;

namespace ProductsPricing.Domain.Products.Entities
{
    public class Ingredient : Entity
    {
        protected Ingredient() { }
        public Ingredient(RefinedProduct rootProduct, Product dependency)
        {
            UpdateRootProduct(rootProduct);
            UpdateDependency(dependency);
        }

        public Guid RootProductId { get; private set; }
        public RefinedProduct RootProduct { get; private set; }
        public Guid DependencyId { get; private set; }
        public Product Dependency { get; private set; }

        private void UpdateRootProduct(RefinedProduct product)
        {
            if (product is null)
            {
                throw new DomainException("O produto base não pode ser nulo");
            }

            RootProductId = product.Id;
            RootProduct = product;
        }

        private void UpdateDependency(Product product)
        {
            if (product is null)
            {
                throw new DomainException("O ingrediente não pode ser nulo");
            }

            DependencyId = product.Id;
            Dependency = product;
        }
    }
}