using FluentAssertions;
using MediatR;
using Moq;
using ProductsPricing.Api.Controllers;
using ProductsPricing.Domain.Users.Commands.Requests;
using ProductsPricing.Domain.Users.Queries.Requests;
using ProductsPricing.Domain.Users.Queries.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductsPricing.Api.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly UsersController _usersController;
        private readonly Mock<IMediator> _requestSender;

        public UsersControllerTests()
        {
            _requestSender = new Mock<IMediator>();
            _usersController = new UsersController(_requestSender.Object);
        }

        [Fact]
        public void Should_throw_when_creating_with_null_sender()
        {
            // Given / When
            var action = () => new UsersController(null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Should_create_user()
        {
            // Given / When
            var request = new CreateUserCommandRequest();
            var result = await _usersController.CreateUserAsync(request);

            // Then
            result.Should().NotBeNull();
            _requestSender.Verify(x => x.Send(request, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Should_login()
        {
            // Given
            var request = new LoginQueryRequest();
            _requestSender.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(new LoginQueryResponse { Token = "token" });

            // When
            var result = await _usersController.LoginAsync(request);

            // Then
            result.Data.Should().NotBeNull();
            _requestSender.Verify(x => x.Send(request, It.IsAny<CancellationToken>()));
        }
    }
}