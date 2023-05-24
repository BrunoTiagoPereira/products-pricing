using FluentValidation;
using ProductsPricing.Domain.Users.Commands.Requests;

namespace ProductsPricing.Domain.Users.Commands.Validators
{
    public class CreateUserCommandRequestValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("O email não é válido.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("A senha é obrigatória.");

            RuleFor(x => x.PasswordConfirmation)
                .Matches(x => x.Password).WithMessage("As senhas são diferentes")
                .When(x => !string.IsNullOrWhiteSpace(x.Password))
                ;
        }
    }
}