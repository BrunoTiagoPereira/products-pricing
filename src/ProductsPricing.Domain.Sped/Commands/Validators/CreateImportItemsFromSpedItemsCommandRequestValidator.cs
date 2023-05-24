using FluentValidation;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.Commands.Validators;

namespace ProductsPricing.Domain.Sped.Commands.Responses
{
    public class CreateImportItemsFromSpedItemsCommandRequestValidator : AbstractValidator<CreateImportItemsFromSpedItemsCommandRequest>
    {
        public CreateImportItemsFromSpedItemsCommandRequestValidator()
        {
            RuleFor(x => x.ImportId).NotEmpty();
            
            RuleFor(x => x.Items).SetValidator(new SpedItemsBaseValidator());
        }
    }
}