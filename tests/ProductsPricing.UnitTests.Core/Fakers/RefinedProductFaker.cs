using Bogus;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class RefinedProductFaker : Faker<RefinedProduct>
    {
        public RefinedProductFaker()
        {
            CustomInstantiator(f =>
            {
                return new RefinedProduct(f.Commerce.Product(), f.Random.Decimal(10, 1000), f.Random.Decimal(10, 1000), new UserFaker().Generate());
            });
        }
    }
}