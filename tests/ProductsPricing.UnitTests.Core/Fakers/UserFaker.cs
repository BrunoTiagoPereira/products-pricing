using Bogus;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class UserFaker : Faker<User>
    {
        public UserFaker()
        {
            CustomInstantiator(f =>
            {
                return new User(f.Internet.Email(), f.Internet.Password());
            });
        }
    }
}