using FluentValidation;
using ProductsPricing.Domain.Imports.Commands.Requests;

namespace ProductsPricing.Domain.Imports.Commands.Validators
{
    public class RecalculateProductValueCommandRequestValidator : AbstractValidator<RecalculateProductValueCommandRequest>
    {
        public RecalculateProductValueCommandRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}