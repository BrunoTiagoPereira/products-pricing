using FluentValidation;
using ProductsPricing.Domain.Sped.ValueObjects;

namespace ProductsPricing.Domain.Sped.Commands.Validators
{
    public class SpedItemsBaseValidator : AbstractValidator<IEnumerable<SpedItem>>
    {
        public SpedItemsBaseValidator()
        {
            RuleFor(x => x).NotNull();
        }
    }
}