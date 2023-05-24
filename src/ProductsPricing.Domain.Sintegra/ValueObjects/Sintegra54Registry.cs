using ProductsPricing.Core.Exceptions;

namespace ProductsPricing.Domain.Sintegra.Dtos
{
    public class Sintegra54Registry : SintegraRegistry
    {
        public const string REGISTRY_CODE = "54";
        const int REGISTRY_MIN_LENGTH = 122;
        private Sintegra54Registry(string productCode, decimal value, decimal additionalValue, decimal ipiValue) : base(productCode)
        {
            UpdateValue(value);
            UpdateAdditionalValue(additionalValue);
            UpdateIpíValue(ipiValue);
        }

        public decimal Value { get; private set; }
        public decimal AdditionalValue { get; private set; }
        public decimal IpiValue { get; private set; }

        public static Sintegra54Registry From(string sintegraLine)
        {
            if (string.IsNullOrWhiteSpace(sintegraLine) ||
                !sintegraLine.StartsWith(REGISTRY_CODE) ||
                sintegraLine.Length < REGISTRY_MIN_LENGTH)
            {
                throw new DomainException($"Linha do sintegra no registro {REGISTRY_CODE} inválida.");
            }

            return new Sintegra54Registry(
                GetStringFromSintegra(sintegraLine, 37, 14),  
                GetDecimalFromSintegra(sintegraLine, 62, 12), 
                GetDecimalFromSintegra(sintegraLine, 74, 12),
                GetDecimalFromSintegra(sintegraLine, 110, 12));
        }

        private void UpdateValue(decimal value)
        {
            if(value < 0)
            {
                throw new DomainException("O valor do produto no registro 54 deve ser maior que 0.");
            }

            Value = value;
        }

        private void UpdateAdditionalValue(decimal additionalValue)
        {
            if (additionalValue < 0)
            {
                throw new DomainException("O valor adicional do produto no registro 54 deve ser maior que 0.");
            }

            AdditionalValue = additionalValue;
        }

        private void UpdateIpíValue(decimal ipiValue)
        {
            if (ipiValue < 0)
            {
                throw new DomainException("O valor do ipi do produto no registro 54 deve ser maior que 0.");
            }

            IpiValue = ipiValue;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ProductCode;
            yield return Value;
            yield return AdditionalValue;
            yield return IpiValue;
        }
    }
}
