using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using System.Linq;
using Xunit;

namespace ProductsPricing.Domain.Tests.Products.Entities
{
    public class PrimaryProductTests
    {
        private readonly PrimaryProduct _item;
        private readonly UnitOfMeasureFaker _unitOfMeasureFaker;

        public PrimaryProductTests()
        {
            _item = new PrimaryProductFaker().Generate();
            _unitOfMeasureFaker = new UnitOfMeasureFaker();
        }
        [Fact]
        public void Should_throw_when_creating_with_null_ncm()
        {
            // Given / When
            var action = () => new PrimaryProduct(null, new UserFaker().Generate(), "product", 10, 10);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_add_conversion_with_null_unit_of_measure()
        {
            // Given / When
            var action = () => _item.AddUnitOfMeasureConversion(null, 1, 1);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_throw_add_conversion_with_invalid_products_count(int productsCount)
        {
            // Given / When
            var action = () => _item.AddUnitOfMeasureConversion(_unitOfMeasureFaker.Generate(), productsCount, 1);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_throw_add_conversion_with_invalid_grams_by_unit(decimal gramsByUnit)
        {
            // Given / When
            var action = () => _item.AddUnitOfMeasureConversion(_unitOfMeasureFaker.Generate(), 1, gramsByUnit);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_adding_same_unit_of_measure_conversion()
        {
            // Given / When
            var unitOfMeasure = _unitOfMeasureFaker.Generate();
            _item.AddUnitOfMeasureConversion(unitOfMeasure, 1, 1);
            var action = () => _item.AddUnitOfMeasureConversion(unitOfMeasure, 1, 1);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_add_unit_of_measure_conversion()
        {
            // Given
            var unitOfmeasure = _unitOfMeasureFaker.Generate();
            var productsCount = 1;
            var gramsByUnit = 1;

            // When
            _item.AddUnitOfMeasureConversion(unitOfmeasure, productsCount, gramsByUnit);

            // Then
            _item.UnitOfMeasureConversions.Should().NotBeEmpty();
            _item.UnitOfMeasureConversions.First().UnitOfMeasure.Should().Be(unitOfmeasure);
            _item.UnitOfMeasureConversions.First().ProductsCount.Should().Be(productsCount);
            _item.UnitOfMeasureConversions.First().GramsByUnit.Should().Be(gramsByUnit);
            _item.UnitOfMeasureConversions.First().ProductId.Should().Be(_item.Id);
            _item.UnitOfMeasureConversions.First().Product.Should().Be(_item);
        }

        [Fact]
        public void Should_create()
        {
            // Given
            var model = new PrimaryProductFaker().Generate();
            var ncm = new NcmFaker().Generate();

            // When
            var primaryProduct = new PrimaryProduct(new[] {ncm}, model.User, model.Name, model.Value, model.AdditionalValue);

            // Then
            primaryProduct.NcmProducts.Should().Contain(x => x.Ncm == ncm);
            primaryProduct.Name.Should().Be(model.Name);
            primaryProduct.Value.Should().Be(model.Value);
            primaryProduct.AdditionalValue.Should().Be(model.AdditionalValue);
            primaryProduct.User.Should().Be(model.User);
        }
    }
}