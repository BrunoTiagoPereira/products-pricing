using Bogus;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class ImpactedByProductFaker : Faker<ImpactedByProduct>
    {
        public ImpactedByProductFaker()
        {
            CustomInstantiator(f =>
            {
                var import = new ImportFaker().Generate();
                var rootProduct = new PrimaryProductFaker().Generate();
                var targetProduct = new RefinedProductFaker().Generate();
                return new ImpactedByProduct(import, rootProduct, targetProduct);
            });
        }
    }
}
