using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.Domain.Users.ValueObjects
{
    public class Role : Enumeration<int>
    {
        public static Role Administrator() => new("Administrator", 1);
        private Role(string name, int value) : base(name, value)
        {
        }

        public bool IsAdministrator() => Value == 1;

    }
}