using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Data.Repositories;
using ProductsPricing.Data.SqlServer.Provider.Persistence;
using ProductsPricing.Domain.Users.Commands.Handlers;
using ProductsPricing.Domain.Users.Commands.Requests;
using ProductsPricing.Domain.Users.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductsPricing.Domain.Tests.Users.Commands.Handlers
{
    public class CreateUserComandHandlerTests
    {
        private readonly DatabaseContext _context;
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Faker _faker;

        public CreateUserComandHandlerTests()
        {
            _context = new DatabaseContextFaker().Generate();
            _userRepository = new(_context);
            _unitOfWork = new UnitOfWork(_context, new Mock<IServiceScopeFactory>().Object);
            _faker = new Faker();
        }

        [Fact]
        public async Task Should_throw_when_email_is_taken()
        {
            // Given / When
            var user = new User(_faker.Internet.Email(), _faker.Internet.Password(8));
            var handler = GetHandler();
            var action = async () => await handler.Handle(new CreateUserCommandRequest
            {
                Email = user.Email.Value,
                Password = user.Password.Hash,
                PasswordConfirmation = user.Password.Hash
            }, CancellationToken.None);

            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            // Then
            await action.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task Should_handle()
        {
            // Given
            var handler = GetHandler();
            var email = _faker.Internet.Email();
            var password = _faker.Internet.Password();

            // When
            await handler.Handle(new CreateUserCommandRequest
            {
                Email = email,
                Password = password,
                PasswordConfirmation = password
            }, CancellationToken.None);

            // Then
            var result = await _userRepository.FindByEmailAsync(email);
            result.Should().NotBeNull();
        }

        public CreateUserCommandHandler GetHandler() => new(_userRepository, _unitOfWork);
    }
}