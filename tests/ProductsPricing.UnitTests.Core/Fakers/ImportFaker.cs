using Bogus;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class ImportFaker : Faker<Import>
    {
        public ImportFaker()
        {
            CustomInstantiator(f =>
            {
                var user = new UserFaker().Generate();
                return new Import(f.System.FileName(), user);
            });
        }
    }
}
