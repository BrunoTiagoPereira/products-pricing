using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using Xunit;

namespace ProductsPricing.Domain.Tests.Products.Entities
{
    public class IngridientTests
    {

        private readonly RefinedProductFaker _refinedProductFaker;
        private readonly PrimaryProductFaker _primaryProductFaker;

        public IngridientTests()
        {
            _refinedProductFaker = new RefinedProductFaker();
            _primaryProductFaker = new PrimaryProductFaker();
        }

        [Fact]
        public void Should_throw_when_creating_with_null_root_product()
        {
            // Given / When
            var action = () => new Ingredient(_refinedProductFaker.Generate(), null);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_creating_with_null_dependency()
        {
            // Given / When
            var action = () => new Ingredient(null, _refinedProductFaker.Generate());

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_create_ingredient()
        {
            // Given
            var rootProduct = _refinedProductFaker.Generate();
            var dependency = _primaryProductFaker.Generate();

            // When
            var ingredient = new Ingredient(rootProduct, dependency);

            // Then
            ingredient.RootProductId.Should().Be(rootProduct.Id);
            ingredient.RootProduct.Should().Be(rootProduct);
            ingredient.Dependency.Should().Be(dependency);
            ingredient.DependencyId.Should().Be(dependency.Id);
        }
    }
}