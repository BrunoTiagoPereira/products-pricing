using FluentAssertions;
using ProductsPricing.Domain.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductsPricing.Domain.Tests.Ncms.ValueObjects
{
    public class CodeTests
    {

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("123456789")]
        [InlineData("1234567a")]
        public void Should_throw_when_invalid_code(string code)
        {
            // Given / When
            var action = () => new Code(code);

            // Then
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Should_create()
        {
            // Given
            var code = "12345678";

            // When
            var entity = new Code(code);

            // Then
            entity.Value.Should().Be(code);
        }
    }
}
