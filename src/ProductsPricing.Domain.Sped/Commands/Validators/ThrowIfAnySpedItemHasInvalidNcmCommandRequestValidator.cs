using FluentValidation;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.Commands.Validators;

namespace ProductsPricing.Domain.Sped.Commands.Responses
{
    public class ThrowIfAnySpedItemHasInvalidNcmCommandRequestValidator : AbstractValidator<ThrowIfAnySpedItemHasInvalidNcmCommandRequest>
    {
        public ThrowIfAnySpedItemHasInvalidNcmCommandRequestValidator()
        {
            RuleFor(x => x.Items).SetValidator(new SpedItemsBaseValidator());
        }
    }
}