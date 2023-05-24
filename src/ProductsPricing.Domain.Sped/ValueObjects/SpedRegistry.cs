using ProductsPricing.Core.Exceptions;

namespace ProductsPricing.Domain.Sped.ValueObjects
{
    public abstract class SpedRegistry
    {
        public string? Code { get; protected set; }

        public const string SPED_DIVISOR = "|";
        protected SpedRegistry()
        {
        }

        protected static void ThrowIfLineIsNotFromRegistry(string line, string registryId)
        {
            if (!line.StartsWith(registryId))
            {
                throw new DomainException($"Linha '{line}' não pertence ao registro '{registryId}'");
            }
        }

        protected static string GetStringValueFromPosition(string line, int position)
        {
            return line.Split(SPED_DIVISOR)[position];
        }

        protected static decimal GetDecimalValueFromPosition(string line, int position)
        {
            var value = GetStringValueFromPosition(line, position);

            var isValueDecimal = decimal.TryParse(value, out var result);

            if (!isValueDecimal)
            {
                throw new ArgumentException($"Não foi possível converter o valor {value}, na linha {line}, em um valor numérico.");
            }

            return result;
        }

    }
}