using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Imports.Contracts;

namespace ProductsPricing.Domain.Sped.ValueObjects
{
    public class SpedItem : ValueObject, IImportable
    {
        public string Ncm { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasureName { get; set; }
        public decimal Value { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal IcmsValue { get; set; }
        public decimal ST { get; set; }
        public decimal IpiValue { get; set; }
        public decimal PisValue { get; set; }
        public decimal CofinsValue { get; set; }
        public int FileLineReference { get; set; }

        public decimal GetValue()
        {
            return Value + IcmsValue + ST + IpiValue + PisValue + CofinsValue - DiscountValue;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Ncm;
            yield return Value;
            yield return DiscountValue;
            yield return IcmsValue;
            yield return IpiValue;
            yield return UnitOfMeasureName;
            yield return PisValue;
            yield return CofinsValue;

        }
    }
}