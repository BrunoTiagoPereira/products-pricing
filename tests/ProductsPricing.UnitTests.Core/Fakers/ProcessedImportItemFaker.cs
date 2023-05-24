using Bogus;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class ProcessedImportItemFaker : Faker<ProcessedImportItem>
    {
        public ProcessedImportItemFaker()
        {
            CustomInstantiator(f =>
            {
                var import = new ImportFaker().Generate();
                var primaryProduct = new PrimaryProductFaker().Generate();
                return new ProcessedImportItem(import, primaryProduct, new UnitOfMeasure("G", import.User), f.Random.Decimal(min: 1), f.Random.Number(min: 1));
            });
        }
    }
}
