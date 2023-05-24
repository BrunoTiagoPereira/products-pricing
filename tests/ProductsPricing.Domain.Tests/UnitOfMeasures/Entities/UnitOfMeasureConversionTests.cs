using FluentAssertions;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using Xunit;

namespace ProductsPricing.Domain.Tests.UnitOfMeasures.Entities
{
    public class UnitOfMeasureConversionTests
    {
        private readonly UnitOfMeasureConversionFaker _unitOfMeasureConversionFaker;

        public UnitOfMeasureConversionTests()
        {
            _unitOfMeasureConversionFaker = new UnitOfMeasureConversionFaker();
        }

        [Fact]
        public void Should_throw_when_invalid_product()
        {
            // Given / When
            var conversion = _unitOfMeasureConversionFaker.Generate();
            var action = () => new UnitOfMeasureConversion(null, conversion.UnitOfMeasure, conversion.ProductsCount, conversion.GramsByUnit);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_invalid_unit_of_mesure()
        {
            // Given / When
            var conversion = _unitOfMeasureConversionFaker.Generate();
            var action = () => new UnitOfMeasureConversion(conversion.Product, null, conversion.ProductsCount, conversion.GramsByUnit);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_throw_when_invalid_products_count(int value)
        {
            // Given / When
            var conversion = _unitOfMeasureConversionFaker.Generate();
            var action = () => new UnitOfMeasureConversion(conversion.Product, conversion.UnitOfMeasure, value, conversion.GramsByUnit);

            // Then
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_throw_when_invalid_grams_by_unit(int value)
        {
            // Given / When
            var conversion = _unitOfMeasureConversionFaker.Generate();
            var action = () => new UnitOfMeasureConversion(conversion.Product, conversion.UnitOfMeasure, conversion.ProductsCount, value);

            // Then
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_create()
        {
            // Given
            var conversion = _unitOfMeasureConversionFaker.Generate();

            // When
            var result = new UnitOfMeasureConversion(conversion.Product, conversion.UnitOfMeasure, conversion.ProductsCount, conversion.GramsByUnit);

            // Then
            result.Product.Should().Be(conversion.Product);
            result.UnitOfMeasure.Should().Be(conversion.UnitOfMeasure);
            result.ProductsCount.Should().Be(conversion.ProductsCount);
            result.GramsByUnit.Should().Be(conversion.GramsByUnit);
        }

        [Fact]
        public void Should_convert()
        {
            // Given
            var ncmConversion = _unitOfMeasureConversionFaker.Generate();
            var value = 1000;
            var productsCount = 10;
            var gramsByUnit = 100;

            // When
            var result = ncmConversion.Convert(value);

            // Then
            result.Should().Be(value / productsCount / gramsByUnit);
        }
    }
}