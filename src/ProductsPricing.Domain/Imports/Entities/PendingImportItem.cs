using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Imports.ValueObjects;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Domain.Imports.Entities
{
    public class PendingImportItem : DecodedImportItem
    {
        protected PendingImportItem() { _pendingProducts = new List<PendingProduct>(); }

        public PendingImportItem(Import import, IEnumerable<PrimaryProduct> pendingProducts, UnitOfMeasure unitOfMeasure, decimal newValue, int fileLineReference ) : base(import, unitOfMeasure, newValue, fileLineReference)
        {
            UpdatePendingProducts(pendingProducts);
            Status = PendingImportItemStatus.Pending();
        }

        public Guid? SelectedProductId { get; private set; }
        public PrimaryProduct? SelectedProduct { get; private set; }

        private List<PendingProduct> _pendingProducts;
        public IReadOnlyCollection<PendingProduct> PendingProducts => _pendingProducts.AsReadOnly();
        public PendingImportItemStatus Status { get; private set; }

        private void UpdatePendingProducts(IEnumerable<PrimaryProduct> pendingProducts)
        {
            if(pendingProducts is null)
            {
                throw new ArgumentNullException(nameof(pendingProducts));
            }

            if (!pendingProducts.Any())
            {
                throw new DomainException("Os produtos pendentes do item são obrigatórios.");
            }

            _pendingProducts = pendingProducts.Select(x => new PendingProduct(this, x)).ToList();
        }

        public void MarkSelectedProduct(PrimaryProduct product)
        {
            if(product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if(!_pendingProducts.Any(x => x.ProductId == product.Id))
            {
                throw new DomainException("O produto deve estar na lista de pendentes.");
            }

            SelectedProductId = product.Id;
            SelectedProduct = product;
            Status = PendingImportItemStatus.Selected();
        }
    }
}
