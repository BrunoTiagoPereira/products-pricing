using FluentValidation.Results;

namespace ProductsPricing.Core.Exceptions
{
    public class DomainException : Exception
    {
        public ICollection<string> ValidationFailuresMessages { get; private set; }

        public DomainException(ICollection<ValidationFailure> failures) : base()
        {
            if (failures is null)
            {
                throw new ArgumentNullException(nameof(failures));
            }
            ValidationFailuresMessages = failures
                .Select(x => x.ErrorMessage)
                .ToList()
                ;
        }

        public DomainException(ICollection<string> errorMessages)
        {
            if (errorMessages is null)
            {
                throw new ArgumentNullException(nameof(errorMessages));
            }

            ValidationFailuresMessages = errorMessages;
        }

        public DomainException(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            ValidationFailuresMessages = new List<string> { errorMessage };
        }
    }
}