using Bogus;

using FluentValidation.TestHelper;
using ProductsPricing.Domain.Users.Queries.Requests;
using ProductsPricing.Domain.Users.Queries.Validators;
using Xunit;

namespace ProductsPricing.Domain.Tests.Users.Queries.Validators
{
    public class LoginQueryRequestValidatorTests
    {
        private readonly Faker _faker = new();

        [Fact]
        public void Should_validate_when_invalid_email()
        {
            // Given
            var validator = new LoginQueryRequestValidator();

            // When
            var result = validator.TestValidate(new LoginQueryRequest { Email = "invalid" });

            // Then
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_validate_when_invalid_password(string password)
        {
            // Given
            var validator = new LoginQueryRequestValidator();

            // When
            var result = validator.TestValidate(new LoginQueryRequest { Password = password });

            // Then
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_not_have_any_validation_errors()
        {
            // Given
            var validator = new LoginQueryRequestValidator();

            // When
            var result = validator.TestValidate(new LoginQueryRequest { Email = _faker.Internet.Email(), Password = _faker.Internet.Password(15) });

            // Then
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}