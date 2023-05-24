using Bogus;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class EvaluatedImportItemFaker : Faker<EvaluatedImportItem>
    {
        public EvaluatedImportItemFaker()
        {
            CustomInstantiator(f =>
            {
                var import = new ImportFaker().Generate();
                var primaryProduct = new PrimaryProductFaker().Generate();
                return new EvaluatedImportItem(import, primaryProduct, f.Random.Decimal(1, 100));
            });
        }
    }
}
