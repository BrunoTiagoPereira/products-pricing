using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Domain.Imports.Entities
{
    public class ProcessedImportItem : DecodedImportItem
    {
        protected ProcessedImportItem() { }

        public ProcessedImportItem(Import import, PrimaryProduct product, UnitOfMeasure unitOfMeasure, decimal newValue,  int fileLineReference) : base(import, unitOfMeasure, newValue, fileLineReference)
        {
            UpdateProduct(product);
        }

        public Guid ProductId { get; private set; }
        public PrimaryProduct Product { get; private set; }
        private void UpdateProduct(PrimaryProduct product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            ProductId = product.Id;
            Product = product;
        }

    }
}
