using Bogus;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class ImpactedProductFaker : Faker<ImpactedProduct>
    {
        public ImpactedProductFaker()
        {
            CustomInstantiator(f =>
            {
                var import = new ImportFaker().Generate();
                var rootProduct = new PrimaryProductFaker().Generate();
                var targetProduct = new PrimaryProductFaker().Generate();
                return new ImpactedProduct(import, rootProduct);
            });
        }
    }
}
