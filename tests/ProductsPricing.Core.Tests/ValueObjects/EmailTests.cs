using Bogus;
using FluentAssertions;
using ProductsPricing.Core.ValueObjects;
using System;
using Xunit;

namespace ProductsPricing.Core.Tests.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("a@")]
        [InlineData("a@asd")]
        public void Should_throw_when_invalid_email(string email)
        {
            // Given / When
            var action = () => new Email(email);

            // Then
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_create()
        {
            // Given / When
            var email = new Faker().Internet.Email();
            var entity = new Email(email);

            // Then
            entity.Value.Should().Be(email);
        }
    }
}