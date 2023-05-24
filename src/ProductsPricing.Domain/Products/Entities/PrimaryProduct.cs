using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Ncms.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.Products.Entities
{
    public class PrimaryProduct : Product
    {
        private List<NcmProduct> _ncmProducts;
        public IReadOnlyCollection<NcmProduct> NcmProducts => _ncmProducts.AsReadOnly();

        private List<UnitOfMeasureConversion> _unitOfMeasureConversions;
        public IReadOnlyCollection<UnitOfMeasureConversion> UnitOfMeasureConversions => _unitOfMeasureConversions.AsReadOnly(); 

        protected PrimaryProduct()
        {
            _ncmProducts = new List<NcmProduct>();
            _unitOfMeasureConversions = new List<UnitOfMeasureConversion>();
        }

        public PrimaryProduct(IEnumerable<Ncm> ncms, User user, string name, decimal cost, decimal additionalValue) : base(name, cost, additionalValue, user)
        {
            _ncmProducts = new List<NcmProduct>();
            _unitOfMeasureConversions = new List<UnitOfMeasureConversion>();
            AddNcms(ncms);
        }

        public void AddNcms(IEnumerable<Ncm> ncms)
        {
            if (ncms is null)
            {
                throw new DomainException("NCMs inválidos.");
            }

            _ncmProducts.AddRange(ncms.Select(x => new NcmProduct(x, this)));
        }

        public void AddUnitOfMeasureConversion(UnitOfMeasure unitOfMeasure, int productsCount, decimal gramsByUnit)
        {
            if(unitOfMeasure is null)
            {
                throw new ArgumentNullException("Unidade de medida inválida.");
            }

            if (productsCount < 1)
            {
                throw new DomainException("A quantidade de produtos deve ser maior que 1.");
            }

            if (gramsByUnit <= 0)
            {
                throw new DomainException("A quantidade de gramas por produto deve ser maior que zero.");
            }

            var alreadyHasUnitOfMeasureConversion = _unitOfMeasureConversions
                .Any(x => x.UnitOfMeasure.Name.Equals(unitOfMeasure.Name, StringComparison.InvariantCultureIgnoreCase));

            if (alreadyHasUnitOfMeasureConversion)
            {
                throw new DomainException("Não é possível adicionar mais de uma conversão para a mesma unidade de medida.");
            }

            _unitOfMeasureConversions.Add(new UnitOfMeasureConversion(this, unitOfMeasure, productsCount, gramsByUnit));
        }

    }
}