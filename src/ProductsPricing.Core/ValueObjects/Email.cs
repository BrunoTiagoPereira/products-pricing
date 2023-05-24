using System.Text.RegularExpressions;

namespace ProductsPricing.Core.ValueObjects
{
    public class Email : ValueObject<string>
    {
        private readonly Regex _emailRegex;

        public Email(string value) : base(value)
        {
            _emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            if (value == string.Empty || !_emailRegex.IsMatch(value))
            {
                throw new ArgumentException("Email inválido.");
            }
        }
    }
}