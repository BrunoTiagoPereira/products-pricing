using Bogus;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class ImportItemFake : ImportItem
    {
        public ImportItemFake(Import import) : base(import)
        {
        }
    }
}
