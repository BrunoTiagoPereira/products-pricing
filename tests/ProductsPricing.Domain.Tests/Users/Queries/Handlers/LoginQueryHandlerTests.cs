using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Data.Repositories;
using ProductsPricing.Data.SqlServer.Provider.Persistence;
using ProductsPricing.Domain.Users.Entities;
using ProductsPricing.Domain.Users.Managers;
using ProductsPricing.Domain.Users.Queries.Handlers;
using ProductsPricing.Domain.Users.Queries.Requests;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductsPricing.Domain.Tests.Users.Queries.Handlers
{
    public class LoginQueryHandlerTests
    {
        private readonly DatabaseContext _context;
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mock<IUserManager> _userManager;
        private readonly Faker _faker;
        private readonly UserFaker _userFaker;

        public LoginQueryHandlerTests()
        {
            _context = new DatabaseContextFaker().Generate();
            _userFaker = new UserFaker();
            _userRepository = new(_context);
            _unitOfWork = new UnitOfWork(_context, new Mock<IServiceScopeFactory>().Object);
            _userManager = new Mock<IUserManager>();
            _faker = new Faker();
        }

        [Fact]
        public void Should_throw_when_creating_with_null_user_repository()
        {
            // Given / When
            var action = () => new LoginQueryHandler(null, _userManager.Object);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_creating_with_null_user_manager()
        {
            // Given / When
            var action = () => new LoginQueryHandler(_userRepository, null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Should_throw_when_user_does_not_exists()
        {
            // Given / When
            var handler = GetHandler();
            var action = async () => await handler.Handle(new LoginQueryRequest { Email = _faker.Internet.Email() }, CancellationToken.None);

            // Then
            await action.Should().ThrowAsync<DomainException>();
            _userManager.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Should_throw_when_invalid_password()
        {
            // Given / When
            var handler = GetHandler();
            var user = _userFaker.Generate();

            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            var action = async () => await handler.Handle(new LoginQueryRequest { Email = user.Email.Value, Password = _faker.Internet.Password() }, CancellationToken.None);

            // Then
            await action.Should().ThrowAsync<DomainException>();
            _userManager.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Should_handle()
        {
            // Given / When
            var handler = GetHandler();
            var password = _faker.Internet.Password(8);
            var user = new User(_faker.Internet.Email(), password);

            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            await handler.Handle(new LoginQueryRequest { Email = user.Email.Value, Password = password }, CancellationToken.None);

            // Then
            _userManager.Verify(x => x.GenerateToken(user), Times.Once);
        }

        public LoginQueryHandler GetHandler() => new(_userRepository, _userManager.Object);
    }
}