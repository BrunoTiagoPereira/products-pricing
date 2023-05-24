using ProductsPricing.Core.Exceptions;
using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.Domain.Sintegra.Dtos
{
    public abstract class SintegraRegistry : ValueObject
    {
        protected SintegraRegistry(string productCode) 
        {
            UpdateProductCode(productCode);
        }
        public string ProductCode { get; private set; }

        private void UpdateProductCode(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                throw new ArgumentNullException("O código do produto deve ser válido no registro do sintegra.");
            }

            ProductCode = productCode;
        }

        protected static string GetStringFromSintegra(string sintegraLine, int startIndex, int length)
        {
            ValidateSintegraSubstring(sintegraLine, startIndex, length);

            return sintegraLine.Substring(startIndex, length);
        }

        protected static decimal GetDecimalFromSintegra(string sintegraLine, int startIndex, int length, int precisionLength = 2)
        {
            var sintegraValue = GetStringFromSintegra(sintegraLine, startIndex, length);

            var num = sintegraValue.Substring(0, sintegraValue.Length - precisionLength);
            var dec = sintegraValue.Substring(sintegraValue.Length - precisionLength, precisionLength);

            return decimal.Parse($"{num},{dec}");
        }

        private static void ValidateSintegraSubstring(string sintegraLine, int startIndex, int length)
        {
            var lineIsEmpty = string.IsNullOrWhiteSpace(sintegraLine);

            if (lineIsEmpty)
            {
                throw new ArgumentNullException("Não foi possível recuperar o valor do sintegra, linha inválida.");
            }

            var invalidStartIndex = startIndex > sintegraLine.Length - 1;
            var invalidLength = (startIndex + length) > sintegraLine.Length - 1;


            if (invalidStartIndex || invalidLength)
            {
                throw new DomainException("Índices inválidos ao buscar um valor no sintegra.");
            }
        }
    }
}
