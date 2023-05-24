using Bogus;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class UnitOfMeasureFaker : Faker<UnitOfMeasure>
    {
        public UnitOfMeasureFaker()
        {
            CustomInstantiator(f =>
            {
                var user = new UserFaker();
                return new UnitOfMeasure("G", user);
            });
        }
    }
}