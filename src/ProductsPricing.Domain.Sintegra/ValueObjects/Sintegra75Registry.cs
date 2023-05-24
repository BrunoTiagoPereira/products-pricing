using ProductsPricing.Core.Exceptions;

namespace ProductsPricing.Domain.Sintegra.Dtos
{
    public class Sintegra75Registry : SintegraRegistry
    {
        public const string REGISTRY_CODE = "75";
        const int REGISTRY_MIN_LENGTH = 99;
        private Sintegra75Registry(string productCode, string ncm, string unitOfMeasure) : base(productCode)
        {
            UpdateNcm(ncm);
            UpdateUnitOfMeasure(unitOfMeasure);
        }

        public string Ncm { get; private set; }
        public string UnitOfMeasure { get; private set; }

        public static Sintegra75Registry From(string sintegraLine)
        {
            if (string.IsNullOrWhiteSpace(sintegraLine) ||
                !sintegraLine.StartsWith(REGISTRY_CODE) ||
                sintegraLine.Length < REGISTRY_MIN_LENGTH)
            {
                throw new DomainException($"Linha do sintegra no registro {REGISTRY_CODE} inválida.");
            }

            return new Sintegra75Registry(GetStringFromSintegra(sintegraLine, 18, 14), GetStringFromSintegra(sintegraLine, 32, 8), GetStringFromSintegra(sintegraLine, 93, 6));
        }

        private void UpdateNcm(string ncm)
        {
            if (string.IsNullOrWhiteSpace(ncm))
            {
                throw new DomainException($"O ncm '{ncm}' não é valido.");
            }

            Ncm = ncm;
        }

        private void UpdateUnitOfMeasure(string unitOfMeasure)
        {
            if(string.IsNullOrWhiteSpace(unitOfMeasure))
            {
                throw new DomainException($"A unidade de medida '{unitOfMeasure}' não é válida.");
            }

            UnitOfMeasure = unitOfMeasure;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ProductCode;
            yield return Ncm;
            yield return UnitOfMeasure;

        }
    }
}
