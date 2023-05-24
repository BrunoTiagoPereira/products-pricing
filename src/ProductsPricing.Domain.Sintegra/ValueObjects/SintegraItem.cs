using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.Domain.Sintegra.Dtos
{
    public class SintegraItem : ValueObject
    {
        public string Ncm { get; set; }
        // 11
        public decimal Value { get; set; }
        // 12
        public decimal AdditionalValue { get; set; }
        // 15
        public decimal IpiValue { get; set; }

        public string UnitOfMeasure { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Ncm;
            yield return Value;
            yield return AdditionalValue;
            yield return IpiValue;
            yield return UnitOfMeasure;
        }
    }
}