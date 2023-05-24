using Bogus;
using ProductsPricing.Domain.Ncms.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class NcmFaker : Faker<Ncm>
    {
        public NcmFaker()
        {
            CustomInstantiator(f =>
            {
                return new Ncm(f.Random.Number(1000, 10000).ToString(), f.Commerce.ProductDescription());
            });
        }
    }
}