using FluentAssertions;
using ProductsPricing.Domain.Users.ValueObjects;
using Xunit;

namespace ProductsPricing.Domain.Tests.Users.ValueObjects
{
    public class RoleTests
    {
        [Fact]
        public void Should_create_administrator()
        {
            // Given // When
            var role = Role.Administrator();

            // Then
            role.Name.Should().Be("Administrator");
            role.Value.Should().Be(1);
        }

        [Fact]
        public void Should_get_when_administrator()
        {
            // Given // When
            var role = Role.Administrator();

            // Then
            role.IsAdministrator().Should().BeTrue();
        }
    }
}