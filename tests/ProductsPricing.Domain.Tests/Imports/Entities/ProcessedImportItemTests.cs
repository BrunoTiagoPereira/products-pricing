using FluentAssertions;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.Entities
{
    public class ProcessedImportItemTests
    {
        private readonly ProcessedImportItemFaker _processedImportItemFaker;
        private readonly ProcessedImportItem _item;

        public ProcessedImportItemTests()
        {
            _processedImportItemFaker = new ProcessedImportItemFaker();
            _item = _processedImportItemFaker.Generate();
        }

        [Fact]
        public void Should_throw_when_invalid_primary_product()
        {
            // Given / When
            var action = () => new ProcessedImportItem(_item.Import, null, _item.UnitOfMeasure, _item.NewValue, _item.FileLineReference);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_create()
        {
            // Given // When
            var result = new ProcessedImportItem(_item.Import, _item.Product, _item.UnitOfMeasure, _item.NewValue, _item.FileLineReference);

            // Then
            result.ImportId.Should().Be(_item.ImportId);
            result.Import.Should().Be(_item.Import);
            result.ProductId.Should().Be(_item.ProductId);
            result.Product.Should().Be(_item.Product);
            result.NewValue.Should().Be(_item.NewValue);
        }
    }
}