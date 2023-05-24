using Bogus;
using FluentAssertions;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Users.Entities;
using System.Linq;
using Xunit;

namespace ProductsPricing.Domain.Tests.Users.Entities
{
    public class UserTests
    {
        private readonly Faker _faker;

        public UserTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Should_not_add_administrator_role_when_already_exists()
        {
            // Given
            var user = GetUser();
            // When
            user.AsAdministrator();
            user.AsAdministrator();

            // Then
            user.Roles.Should().ContainSingle();
        }

        [Fact]
        public void Should_make_user_administrator()
        {
            // Given
            var user = GetUser();
            // When
            user.AsAdministrator();

            // Then
            user.Roles.Should().ContainSingle();
            user.Roles.First().IsAdministrator().Should().BeTrue();
        }

        [Fact]
        public void Should_create()
        {
            // Given
            var email = _faker.Internet.Email();
            var password = _faker.Internet.Password();

            // When
            var result = new User(email, password);

            // Then
            result.Email.Value.Should().Be(email);
            result.Password.Hash.Should().Be(new Password(password).Hash);
            result.Roles.Should().NotBeNull();
            result.Products.Should().BeEmpty();
            result.Imports.Should().BeEmpty();
        }

        [Fact]
        public void Should_update_email()
        {
            // Given
            var user = GetUser();
            var email = _faker.Internet.Email();

            // When
            user.UpdateEmail(email);

            // Then
            user.Email.Value.Should().Be(email);
        }

        [Fact]
        public void Should_update_password_hash()
        {
            // Given
            var user = GetUser();
            var password = _faker.Internet.Password();

            // When
            user.UpdatePassword(password);

            // Then
            user.Password.Hash.Should().Be(new Password(password).Hash);
        }

        private User GetUser() => new(_faker.Internet.Email(), _faker.Internet.Password());
    }
}