using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Domain.Imports.Entities
{
    public class PendingProduct : Entity
    {
        protected PendingProduct() { }
        public PendingProduct(PendingImportItem pendingImportItem, PrimaryProduct product)
        {
            UpdatePendingImportItem(pendingImportItem);
            UpdateProduct(product);
        }

        public Guid PendingImportItemId { get; private set; }
        public PendingImportItem PendingImportItem { get; private set; }

        public Guid ProductId { get; private set; }
        public PrimaryProduct Product { get; private set; }

        private void UpdatePendingImportItem(PendingImportItem pendingImportItem)
        {
            if (pendingImportItem is null)
            {
                throw new ArgumentNullException(nameof(pendingImportItem));
            }

            PendingImportItemId = pendingImportItem.Id;
            PendingImportItem = pendingImportItem;
        }

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
