using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Domain.Imports.Entities
{
    public class ImpactedByProduct : ImpactedProduct
    {
        public ImpactedByProduct(Import import, Product rootProduct, Product targetProduct) : base(import, targetProduct)
        {
            UpdateRootProduct(rootProduct);
        }
        protected ImpactedByProduct() { }
        public Guid RootProductId { get; private set; }
        public Product RootProduct { get; private set; }
        private void UpdateRootProduct(Product rootProduct)
        {
            if (rootProduct is null)
            {
                throw new ArgumentNullException(nameof(rootProduct));
            }

            RootProductId = rootProduct.Id;
            RootProduct = rootProduct;
        }

    }
}
