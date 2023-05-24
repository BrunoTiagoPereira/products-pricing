using FluentValidation;
using ProductsPricing.Domain.Imports.Commands.Requests;

namespace ProductsPricing.Domain.Imports.Commands.Validators
{
    public class CreateRecalculationJobForImportEvaluatedItemsCommandRequestValidator : AbstractValidator<CreateRecalculationJobForImportEvaluatedItemsCommandRequest>
    {
        public CreateRecalculationJobForImportEvaluatedItemsCommandRequestValidator()
        {
            RuleFor(x => x.ImportId).NotEmpty();
        }
    }
}