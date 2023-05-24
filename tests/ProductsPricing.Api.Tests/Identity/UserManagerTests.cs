using System;

using Bogus;

using FluentAssertions;

using Microsoft.Extensions.Configuration;

using Moq;
using ProductsPricing.Api.Identity;
using ProductsPricing.Domain.Users.Entities;
using Xunit;

namespace ProductsPricing.Api.Tests.Identity
{
    public class UserManagerTests
    {
        private readonly Faker _faker;
        public UserManagerTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Should_generate_token()
        {
            // Given
            var configuration = new Mock<IConfiguration>();
            var section = new Mock<IConfigurationSection>();
            var user = new User(_faker.Internet.Email(), _faker.Internet.Password(8));

            configuration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(section.Object);
            section.Setup(x => x.Value).Returns(Guid.NewGuid().ToString());

            // When
            var result = new UserManager(configuration.Object).GenerateToken(user);

            // Then
            result.Should().NotBeNullOrWhiteSpace();
        }
    }
}