using FluentValidation;

namespace ProductsPricing.Domain.Sped.Commands.Requests
{
    public class AddWarningImportLogForSpedItemsWithNoNcmCommandRequestValidator : AbstractValidator<AddWarningImportLogForSpedItemsWithNoNcmCommandRequest>
    {
        public AddWarningImportLogForSpedItemsWithNoNcmCommandRequestValidator()
        {
            RuleFor(x => x.ImportId).NotEmpty();

            RuleFor(x => x.SpedItems).NotNull();
        }
    }
}