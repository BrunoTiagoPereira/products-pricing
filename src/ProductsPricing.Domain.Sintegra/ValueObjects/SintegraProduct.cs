using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Ncms.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Domain.Sintegra.ValueObjects
{
    public class SintegraProduct : ValueObject
    {
        public Ncm Ncm { get; set; }
        // 11
        public decimal Value { get; set; }
        // 12
        public decimal AdditionalValue { get; set; }
        // 15
        public decimal IpiValue { get; set; }

        public UnitOfMeasure UnitOfMeasure { get; set; }

        public decimal GetValue()
        {
            decimal value = Value;

            //if (Ncm.NcmConversion is not null)
            //{
            //    value = Ncm.NcmConversion.Convert(Value);
            //}

            return value + AdditionalValue + IpiValue;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Ncm;
            yield return Value;
            yield return UnitOfMeasure;
            yield return IpiValue;
        }
    }
}