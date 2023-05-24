using FluentValidation;
using ProductsPricing.Domain.Imports.Commands.Requests;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Domain.Imports.Commands.Validators
{
    public class CreateImportCommandRequestValidator : AbstractValidator<CreateImportCommandRequest>
    {
        public CreateImportCommandRequestValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage("O nome do arquivo deve ser válido.");

            When(x => !string.IsNullOrWhiteSpace(x.FileName), () =>
            {
                RuleFor(x => x.FileName).MaximumLength(Import.FILE_NAME_MAX_LENGTH).WithMessage("O nome do arquivo deve ter no máximo {MaxLength} caracteres.");
            });
        }
    }
}