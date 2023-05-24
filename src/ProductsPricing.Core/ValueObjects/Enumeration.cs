namespace ProductsPricing.Core.ValueObjects
{
    public abstract class Enumeration<T> : ValueObject<T>
    {
        public string Name { get; private init; }
        public Enumeration(string name, T value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }

    public abstract class Enumeration : Enumeration<int>
    {
        protected Enumeration(string name, int value) : base(name, value)
        {
        }
    }
}