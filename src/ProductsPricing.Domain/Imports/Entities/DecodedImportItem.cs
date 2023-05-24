using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Domain.Imports.Entities
{
    public abstract class DecodedImportItem : ImportItem
    {
        protected DecodedImportItem() { }

        protected DecodedImportItem(Import import, UnitOfMeasure unitOfMeasure, decimal newValue, int fileLineReference) : base(import)
        {
            UpdateNewValue(newValue);
            UpdateUnitOfMeasure(unitOfMeasure);
            UpdateFileLineReference(fileLineReference);
        }

        public decimal NewValue { get; private set; }
        public int FileLineReference { get; private set; }
        public Guid UnitOfMeasureId { get; private set; }
        public UnitOfMeasure UnitOfMeasure { get; private set; }

        private void UpdateNewValue(decimal newValue)
        {
            if (newValue <= 0)
            {
                throw new DomainException(nameof(newValue));
            }

            NewValue = newValue;
        }

        private void UpdateUnitOfMeasure(UnitOfMeasure unitOfMeasure)
        {
            if (unitOfMeasure is null)
            {
                throw new ArgumentNullException(nameof(unitOfMeasure));
            }

            UnitOfMeasureId = unitOfMeasure.Id;
            UnitOfMeasure = unitOfMeasure;
        }

        private void UpdateFileLineReference(int fileLineReference)
        {
            if (fileLineReference < 1)
            {
                throw new DomainException(nameof(fileLineReference));
            }

            FileLineReference = fileLineReference;
        }
    }
}
