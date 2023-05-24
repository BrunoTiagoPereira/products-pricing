using FluentAssertions;
using FluentValidation.Results;
using ProductsPricing.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductsPricing.Core.Tests.Exceptions
{
    public class DomainExceptionTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_throw_when_creating_with_invalid_error_message(string errorMessage)
        {
            // Given / When
            var action = () => new DomainException(errorMessage);

            // Then
            action.Should().Throw<ArgumentNullException>();   
        }

        [Fact]
        public void Should_throw_when_creating_with_null_error_messages()
        {
            // Given / When
            var action = () => new DomainException(errorMessages: null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_creating_with_null_failures()
        {
            // Given / When
            var action = () => new DomainException(failures: null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_create_with_error_message()
        {
            // Given / When
            var errorMessage = "error";
            var exception = new DomainException(errorMessage);

            // Then
            exception.ValidationFailuresMessages.Should().NotBeEmpty();
            exception.ValidationFailuresMessages.First().Should().Be(errorMessage);
        }

        [Fact]
        public void Should_create_with_error_messages()
        {
            // Given / When
            var errorMessages = new[] { "error", "error2" };
            var exception = new DomainException(errorMessages);

            // Then
            exception.ValidationFailuresMessages.Should().NotBeEmpty();
            exception.ValidationFailuresMessages.Should().HaveCount(2);
            exception.ValidationFailuresMessages.Should().BeEquivalentTo(errorMessages);
        }

        [Fact]
        public void Should_create_with_failures()
        {
            // Given / When
            var errorMessages = new ValidationFailure[] 
            {
                new ValidationFailure("property1", "error1"), new ValidationFailure("property2", "error2") 
            };
            var exception = new DomainException(errorMessages);

            // Then
            exception.ValidationFailuresMessages.Should().NotBeEmpty();
            exception.ValidationFailuresMessages.Should().HaveCount(2);
            exception.ValidationFailuresMessages.Should().BeEquivalentTo(errorMessages.Select(x => x.ErrorMessage));
        }
    }
}
