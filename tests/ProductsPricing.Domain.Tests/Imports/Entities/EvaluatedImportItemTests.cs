using Bogus;
using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.Entities
{
    public class EvaluatedImportItemTests
    {
        private readonly EvaluatedImportItemFaker _evaluatedImportItemFaker;
        private readonly EvaluatedImportItem _item;

        public EvaluatedImportItemTests()
        {
            _evaluatedImportItemFaker = new EvaluatedImportItemFaker();
            _item = _evaluatedImportItemFaker.Generate();
        }

        [Fact]
        public void Should_throw_when_invalid_import()
        {
            // Given / When
            var action = () => new EvaluatedImportItem(null, _item.Product, _item.NewValue);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_invalid_primary_product()
        {
            // Given / When
            var action = () => new EvaluatedImportItem(_item.Import, null, _item.NewValue);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_invalid_new_value()
        {
            // Given / When
            var action = () => new EvaluatedImportItem(_item.Import, _item.Product, 0);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_create()
        {
            // Given // When
            var result = new EvaluatedImportItem(_item.Import, _item.Product, _item.NewValue);

            // Then
            result.ImportId.Should().Be(_item.ImportId);
            result.Import.Should().Be(_item.Import);
            result.ProductId.Should().Be(_item.ProductId);
            result.Product.Should().Be(_item.Product);
            result.NewValue.Should().Be(_item.NewValue);
        }
    }
}