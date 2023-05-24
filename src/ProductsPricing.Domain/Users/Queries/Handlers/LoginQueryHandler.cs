using MediatR;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Users.Managers;
using ProductsPricing.Domain.Users.Queries.Requests;
using ProductsPricing.Domain.Users.Queries.Responses;

namespace ProductsPricing.Domain.Users.Queries.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserManager _userManager;

        public LoginQueryHandler(IUserRepository userRepository, IUserManager userManager)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);

            if (user is null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            var isPasswordInvalid = new Password(request.Password) != user.Password;

            if (isPasswordInvalid)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            return new LoginQueryResponse { Token = _userManager.GenerateToken(user) };
        }
    }
}