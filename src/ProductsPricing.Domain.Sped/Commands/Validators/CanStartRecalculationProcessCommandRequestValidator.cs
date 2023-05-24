using FluentValidation;
using ProductsPricing.Domain.Sped.Commands.Requests;

namespace ProductsPricing.Domain.Sped.Commands.Responses
{
    public class CanStartRecalculationProcessCommandRequestValidator : AbstractValidator<CanStartRecalculationProcessCommandRequest>
    {
        public CanStartRecalculationProcessCommandRequestValidator()
        {
            RuleFor(x => x.ImportId).NotEmpty();
        }
    }
}