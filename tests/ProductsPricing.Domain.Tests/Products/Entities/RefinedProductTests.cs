using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System.Linq;
using Xunit;

namespace ProductsPricing.Domain.Tests.Products.Entities
{
    public class RefinedProductTests
    {
        private readonly RefinedProductFaker _refinedProductFaker;
        private readonly PrimaryProductFaker _primaryProductFaker;

        public RefinedProductTests()
        {
            _refinedProductFaker = new RefinedProductFaker();
            _primaryProductFaker = new PrimaryProductFaker();
        }


        [Fact]
        public void Should_throw_when_add_ingredient_with_loop_dependency()
        {
            // Given / When

            var refinedProduct = _refinedProductFaker.Generate();
            var action = () => refinedProduct.AddIngredient(refinedProduct);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_add_null_dependency()
        {
            // Given / When
            var action = () => _refinedProductFaker.Generate().AddIngredient(null);

            // Then
            action.Should().Throw<DomainException>();
        }


        [Fact]
        public void Should_throw_when_add_null_dependencies()
        {
            // Given / When
            var action = () => _refinedProductFaker.Generate().AddIngredients(null);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_remove_with_null_dependency()
        {
            // Given / When
            var action = () => _refinedProductFaker.Generate().RemoveIngredient(null);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_add_ingredient()
        {
            // Given
            var refinedProduct = _refinedProductFaker.Generate();
            var primaryProduct = _primaryProductFaker.Generate();

            // When
            refinedProduct.AddIngredient(primaryProduct);

            // Then
            refinedProduct.Ingredients.Should().ContainSingle();
            refinedProduct.Ingredients.First().DependencyId.Should().Be(primaryProduct.Id);
            refinedProduct.Ingredients.First().Dependency.Should().Be(primaryProduct);
        }

        [Fact]
        public void Should_add_ingredients()
        {
            // Given
            var refinedProduct = _refinedProductFaker.Generate();
            var primaryProduct = _primaryProductFaker.Generate();
            var primaryProduct2 = _primaryProductFaker.Generate();

            // When
            refinedProduct.AddIngredients(new[] {primaryProduct, primaryProduct2});

            // Then
            refinedProduct.Ingredients.Should().HaveCount(2);
            refinedProduct.Ingredients.Should().Contain(x => x.DependencyId == primaryProduct.Id);
            refinedProduct.Ingredients.Should().Contain(x => x.DependencyId == primaryProduct2.Id);
        }

        [Fact]
        public void Should_not_removeingredient_when_does_not_exists()
        {
            // Given
            var refinedProduct = _refinedProductFaker.Generate();
            var primaryProduct = _primaryProductFaker.Generate();
            var primaryProduct2 = _primaryProductFaker.Generate();
            refinedProduct.AddIngredient(primaryProduct);

            // When
            refinedProduct.RemoveIngredient(primaryProduct2);

            // Then
            refinedProduct.Ingredients.Should().ContainSingle();
        }

        [Fact]
        public void Should_remove_ingredient()
        {
            // Given
            var refinedProduct = _refinedProductFaker.Generate();
            var primaryProduct = _primaryProductFaker.Generate();
            refinedProduct.AddIngredient(primaryProduct);

            // When
            refinedProduct.RemoveIngredient(primaryProduct);

            // Then
            refinedProduct.Ingredients.Should().BeEmpty();
        }

    }
}