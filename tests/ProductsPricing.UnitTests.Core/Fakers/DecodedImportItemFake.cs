using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class DecodedImportItemFake : DecodedImportItem
    {
        public DecodedImportItemFake(Import import, UnitOfMeasure unitOfMeasure, decimal newValue, int fileLineReference) : base(import, unitOfMeasure, newValue, fileLineReference)
        {
        }
    }
}