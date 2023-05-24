using Bogus;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class IngridientFaker : Faker<Ingredient>
    {
        public IngridientFaker()
        {
            CustomInstantiator(f =>
            {
                var refinedProductFaker = new RefinedProductFaker();
                var primaryProductFaker = new PrimaryProductFaker();
                return new Ingredient(refinedProductFaker.Generate(), primaryProductFaker.Generate());
            });
        }
    }
}