using FluentValidation;
using MediatR;
using ProductsPricing.Domain.Sintegra.Commands.Responses;
using ProductsPricing.Domain.Sintegra.Dtos;

namespace ProductsPricing.Domain.Sintegra.Commands.Requests
{
    public class CreateSintegraProductsFromItemsCommandValidator : AbstractValidator<CreateSintegraProductsFromItemsCommandRequest>
    {
    }
}
