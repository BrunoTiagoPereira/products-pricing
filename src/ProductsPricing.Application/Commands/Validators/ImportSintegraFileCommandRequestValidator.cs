using FluentValidation;
using ProductsPricing.Application.Commands.Requests;

namespace ProductsPricing.Application.Commands.Validators
{
    public class ImportSintegraFileCommandRequestValidator : AbstractValidator<ImportSpedFileCommandRequest>
    {
        public ImportSintegraFileCommandRequestValidator()
        {
            RuleFor(x => x.File).NotNull().WithMessage("O arquivo deve ser válido.");

            RuleFor(x => x.File.FileName).NotNull().NotEmpty().WithMessage("O nome do arquivo deve ser válido.");

            RuleFor(x => x.File.Length).GreaterThan(0).WithMessage("O arquivo vazio.");
        }
    }
}