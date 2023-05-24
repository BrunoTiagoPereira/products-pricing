using FluentValidation;
using ProductsPricing.Domain.Imports.Commands.Requests;

namespace ProductsPricing.Domain.Imports.Commands.Validators
{
    public class UpdateProductValuesFromEvaluatedItemsCommandRequestValidator : AbstractValidator<UpdateProductValuesFromEvaluatedItemsCommandRequest>
    {
        public UpdateProductValuesFromEvaluatedItemsCommandRequestValidator()
        {
            RuleFor(x => x.ImportId).NotEmpty();
        }
    }
}