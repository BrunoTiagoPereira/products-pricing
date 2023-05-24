using FluentValidation;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.Commands.Validators;

namespace ProductsPricing.Domain.Sped.Commands.Responses
{
    public class ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandRequestValidator : AbstractValidator<ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandRequest>
    {
        public ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandRequestValidator()
        {
            RuleFor(x => x.Items).SetValidator(new SpedItemsBaseValidator());

            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}