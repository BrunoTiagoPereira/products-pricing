using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Domain.UnitOfMeasures.Entities
{
    public class UnitOfMeasureConversion : Entity
    {
        protected UnitOfMeasureConversion() { }
        public UnitOfMeasureConversion(PrimaryProduct product, UnitOfMeasure unitOfMeasure, int productsCount, decimal gramsByUnit)
        {
            UpdateUnitOfMeasure(unitOfMeasure);
            UpdateProductsCount(productsCount);
            UpdateGramsByUnit(gramsByUnit);
            UpdateProduct(product);
        }

        public int ProductsCount { get; private set; }

        public decimal GramsByUnit { get; set; }

        public Guid ProductId { get; private set; }

        public Guid UnitOfMeasureId { get; private set; }
        public UnitOfMeasure UnitOfMeasure { get; private set; }

        public PrimaryProduct Product { get; private set; }

        private void UpdateUnitOfMeasure(UnitOfMeasure unitOfMeasure)
        {
            if (unitOfMeasure is null)
            {
                throw new ArgumentNullException("A unidade de medida deve ser válida.");
            }

            UnitOfMeasureId = unitOfMeasure.Id;
            UnitOfMeasure = unitOfMeasure;
        }

        private void UpdateProductsCount(int productsCount)
        {
            if (productsCount < 1)
            {
                throw new ArgumentException("A quantidade de produtos deve ser maior que 0.");
            }

            ProductsCount = productsCount;
        }

        private void UpdateGramsByUnit(decimal gramsByUnit)
        {
            if (gramsByUnit < 1)
            {
                throw new ArgumentException("A quantidade de gramas por unidade deve ser maior que 0.");
            }

            GramsByUnit = gramsByUnit;
        }
        private void UpdateProduct(PrimaryProduct product)
        {
            if (product is null)
            {
                throw new ArgumentNullException("O produto deve ser válido.");
            }

            ProductId = product.Id;
            Product = product;
        }

        public decimal Convert(decimal value)
        {
            return value / ProductsCount / GramsByUnit;
        }
    }
}