using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Products.ValueObjects;

namespace ProductsPricing.Domain.Ncms.Entities
{
    public class Ncm : AggregateRoot
    {
        public const int MAX_NCM_DESCRIPTION_LENGTH = 2000;
        public Code Code { get; private set; }
        public string Description { get; private set; }

        protected Ncm()
        {
        }

        public Ncm(string code, string description)
        {
            UpdateCode(code);
            UpdateDescription(description);
        }

        private void UpdateCode(string code)
        {
            Code = new Code(code);
        }

        private void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new DomainException("Descrição inválida.");
            }

            if(description.Length > MAX_NCM_DESCRIPTION_LENGTH)
            {
                throw new DomainException($"A descrição deve ter menos que {MAX_NCM_DESCRIPTION_LENGTH} caracteres.");
            }

            Description = description;
        }
    }
}