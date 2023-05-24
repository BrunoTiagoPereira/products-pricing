using Bogus;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class UnitOfMeasureConversionFaker : Faker<UnitOfMeasureConversion>
    {
        public UnitOfMeasureConversionFaker()
        {
            CustomInstantiator(f =>
            {
                var user = new UserFaker();
                var primaryProduct = new PrimaryProductFaker().Generate();
                
                return new UnitOfMeasureConversion(primaryProduct, new UnitOfMeasure("KG", user.Generate()), 1, 1000);
            });
        }
    }
}