using FluentAssertions;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.Entities
{
    public class PendingProductTests
    {
        private readonly PendingImportItemFaker _pendingImportItemFaker;
        private readonly PrimaryProductFaker _primaryProductFaker;
        private readonly PendingProduct _item;

        public PendingProductTests()
        {
            _pendingImportItemFaker = new PendingImportItemFaker();
            _primaryProductFaker = new PrimaryProductFaker();
            _item = new PendingProduct(_pendingImportItemFaker.Generate(), _primaryProductFaker.Generate());
        }

        [Fact]
        public void Should_throw_when_invalid_pending_import_item()
        {
            // Given / When
            var action = () => new PendingProduct(null, _item.Product);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_invalid_primary_product()
        {
            // Given / When
            var action = () => new PendingProduct(_item.PendingImportItem, null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_create()
        {
            // Given // When
            var result = new PendingProduct(_item.PendingImportItem, _item.Product);

            // Then
            result.PendingImportItemId.Should().Be(_item.PendingImportItemId);
            result.PendingImportItem.Should().Be(_item.PendingImportItem);
            result.ProductId.Should().Be(_item.ProductId);
            result.Product.Should().Be(_item.Product);
        }
    }
}