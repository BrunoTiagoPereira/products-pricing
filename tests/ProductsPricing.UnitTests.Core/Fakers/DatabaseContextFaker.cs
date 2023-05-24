using Bogus;
using Microsoft.EntityFrameworkCore;
using ProductsPricing.Data.SqlServer.Provider.Persistence;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class DatabaseContextFaker : Faker<DatabaseContext>
    {
        public DatabaseContextFaker()
        {
            CustomInstantiator(x =>
            {
                var builder = new DbContextOptionsBuilder<DatabaseContext>();
                builder.UseInMemoryDatabase(x.Random.Guid().ToString());

                return new DatabaseContext(builder.Options);
            });
        }
    }
}