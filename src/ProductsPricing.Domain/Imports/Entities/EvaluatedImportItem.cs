using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Domain.Imports.Entities
{
    public class EvaluatedImportItem : ImportItem
    {
        protected EvaluatedImportItem() { }
        public EvaluatedImportItem(Import import, PrimaryProduct primaryProduct, decimal newValue) : base(import)
        {
            UpdatePrimaryProduct(primaryProduct);
            UpdateNewValue(newValue);
        }
        public Guid ProductId { get; private set; }
        public PrimaryProduct Product { get; private set; }
        public decimal NewValue { get; private set; }
        private void UpdatePrimaryProduct(PrimaryProduct primaryProduct)
        {
            if (primaryProduct is null)
            {
                throw new ArgumentNullException("O produto deve ser válido.");
            }

            ProductId = primaryProduct.Id;
            Product = primaryProduct;
        }
        private void UpdateNewValue(decimal newValue)
        {
            if (newValue < 1)
            {
                throw new DomainException("O valor novo do item não pode ser menor que 1.");
            }

            NewValue = newValue;
        }
    }
}
