using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class EnumerationFake<T> : Enumeration<T>
    {
        public EnumerationFake(string name, T value) : base(name, value)
        {
        }
    }
}