using MediatR;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Users.Commands.Requests;
using ProductsPricing.Domain.Users.Commands.Responses;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.Users.Commands.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var emailIsTaken = await _userRepository.EmailIsTakenAsync(request.Email);

            if (emailIsTaken)
            {
                throw new DomainException("O email está em uso.");
            }

            await _userRepository.AddAsync(new User(request.Email, request.Password));
            await _uow.CommitAsync();

            return new CreateUserCommandResponse();
        }
    }
}