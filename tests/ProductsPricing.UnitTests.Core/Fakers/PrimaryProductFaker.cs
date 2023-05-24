using Bogus;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class PrimaryProductFaker : Faker<PrimaryProduct>
    {
        public PrimaryProductFaker()
        {
            CustomInstantiator(f =>
            {
                return new PrimaryProduct(new[] { new NcmFaker().Generate() }, new UserFaker().Generate(), f.Commerce.Product(), f.Random.Decimal(10, 1000), f.Random.Decimal(10, 1000));
            });
        }
    }
}