using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class ValueObjectFake : ValueObject<string>
    {
        public ValueObjectFake(string value) : base(value)
        {
        }
    }
}