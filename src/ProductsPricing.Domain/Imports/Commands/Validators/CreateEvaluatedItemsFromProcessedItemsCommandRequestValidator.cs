using FluentValidation;
using ProductsPricing.Domain.Imports.Commands.Requests;

namespace ProductsPricing.Domain.Imports.Commands.Validators
{
    public class CreateEvaluatedItemsFromProcessedItemsCommandRequestValidator : AbstractValidator<CreateEvaluatedItemsFromProcessedItemsCommandRequest>
    {
        public CreateEvaluatedItemsFromProcessedItemsCommandRequestValidator()
        {
            RuleFor(x => x.ImportId).NotEmpty();
        }
    }
}