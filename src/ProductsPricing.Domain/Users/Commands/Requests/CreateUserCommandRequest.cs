using MediatR;
using ProductsPricing.Domain.Users.Commands.Responses;

namespace ProductsPricing.Domain.Users.Commands.Requests
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

    }
}