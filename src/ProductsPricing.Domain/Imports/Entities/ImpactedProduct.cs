using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Imports.ValueObjects;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Domain.Imports.Entities
{
    public class ImpactedProduct : Entity
    {
        public ImpactedProduct(Import import, Product targetProduct)
        {
            UpdateImport(import);
            UpdateTargetProduct(targetProduct);
            Status = ImpactedProductStatus.Pending();
        }
        protected ImpactedProduct() { }
        public Guid ImportId { get; private set; }
        public Import Import { get; private set; }
        public Guid TargetProductId { get; private set; }
        public Product TargetProduct { get; private set; }
        public ImpactedProductStatus Status { get; private set; }

        public void MarkAsRecalculated()
        {
            Status = ImpactedProductStatus.Recalculated();
        }
        private void UpdateImport(Import import)
        {
            if(import is null)
            {
                throw new ArgumentNullException(nameof(import));
            }

            ImportId = import.Id;
            Import = import;
        }
        private void UpdateTargetProduct(Product targetProduct)
        {
            if (targetProduct is null)
            {
                throw new ArgumentNullException(nameof(targetProduct));
            }

            TargetProductId = targetProduct.Id;
            TargetProduct = targetProduct;
        }

    }
}
