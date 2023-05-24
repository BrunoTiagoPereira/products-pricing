using ProductsPricing.Core.Exceptions;
using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.Domain.Products.ValueObjects
{
    public class Code : ValueObject
    {
        public const int CODE_MAX_LENGTH = 8;
        protected Code() { }
        public Code(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new DomainException("O código do ncm não pode ser nulo");
            }

            if(code.Length > CODE_MAX_LENGTH)
            {
                throw new DomainException("O código NCM deve ter no máximo 8 dígitos");
            }

            if (code.ToCharArray().Any(x => !char.IsDigit(x)))
            {
                throw new DomainException("O código NCM deve ter apenas números");
            }

            Value = code;
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}