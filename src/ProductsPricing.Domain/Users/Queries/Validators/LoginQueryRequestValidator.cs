using FluentValidation;
using ProductsPricing.Domain.Users.Queries.Requests;

namespace ProductsPricing.Domain.Users.Queries.Validators
{
    public class LoginQueryRequestValidator : AbstractValidator<LoginQueryRequest>
    {
        public LoginQueryRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("A senha é obrigatória.");
        }
    }
}