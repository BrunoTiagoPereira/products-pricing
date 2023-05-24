using ProductsPricing.Core.DomainObjects;

namespace ProductsPricing.Domain.Imports.Entities
{
    public abstract class ImportItem : Entity
    {
        protected ImportItem() { }
        public ImportItem(Import import)
        {
            UpdateImport(import);
        }
        public Guid ImportId { get; private set; }
        public Import Import { get; private set; }
        private void UpdateImport(Import import)
        {
            if (import is null)
            {
                throw new ArgumentNullException("A importação deve ser válida.");
            }

            ImportId = import.Id;
            Import = import;
        }
    }
}
