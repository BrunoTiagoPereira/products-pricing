using Bogus;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class DecodedImportItemFaker : Faker<DecodedImportItem>
    {
        public DecodedImportItemFaker()
        {
            CustomInstantiator(f =>
            {
                var import = new ImportFaker().Generate();
                return new DecodedImportItemFake(import, new UnitOfMeasure("G", import.User), f.Random.Decimal(min: 1), f.Random.Number(min: 1));
            });
        }
    }
}
