using FluentValidation;
using ProductsPricing.Domain.Imports.Commands.Requests;

namespace ProductsPricing.Domain.Imports.Commands.Validators
{
    public class AddImportLogCommandRequestValidator : AbstractValidator<AddImportLogCommandRequest>
    {
        public AddImportLogCommandRequestValidator()
        {
            RuleFor(x => x.ImportId).NotEmpty();

            RuleFor(x => x.LogLevel).NotNull();

            RuleFor(x => x.Message).NotEmpty();
        }
    }
}