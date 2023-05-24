using FluentValidation;
using ProductsPricing.Domain.Sped.Commands.Requests;

namespace ProductsPricing.Domain.Sped.Commands.Responses
{
    public class ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandRequestValidator : AbstractValidator<ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandRequest>
    {
        public ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandRequestValidator()
        {
            RuleFor(x => x.ImportId).NotEmpty();
        }
    }
}