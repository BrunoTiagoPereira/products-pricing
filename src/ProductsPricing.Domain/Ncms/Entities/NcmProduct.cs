using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Domain.Ncms.Entities
{
    public class NcmProduct : Entity
    {
        protected NcmProduct() { }
        public NcmProduct(Ncm ncm, PrimaryProduct product)
        {
            UpdateNcm(ncm);
            UpdateProduct(product);
        }

        public Guid NcmId { get; private set; }
        public Ncm Ncm { get; private set; }

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

        private void UpdateNcm(Ncm ncm)
        {
            if (ncm is null)
            {
                throw new ArgumentNullException(nameof(ncm));
            }

            NcmId = ncm.Id;
            Ncm = ncm;
        }
    }
}
