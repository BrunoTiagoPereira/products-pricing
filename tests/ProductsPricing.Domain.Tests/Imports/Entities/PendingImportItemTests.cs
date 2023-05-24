using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using System.Linq;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.Entities
{
    public class PendingImportItemTests
    {
        private readonly PendingImportItemFaker _pendingImportItemFaker;
        private readonly PrimaryProductFaker _primaryProductFaker;
        private readonly PendingImportItem _item;

        public PendingImportItemTests()
        {
            _pendingImportItemFaker = new PendingImportItemFaker();
            _primaryProductFaker = new PrimaryProductFaker();
            _item = _pendingImportItemFaker.Generate();
        }

        [Fact]
        public void Should_throw_when_invalid_import()
        {
            // Given / When
            var action = () => new PendingImportItem(null, _item.PendingProducts.Select(x => x.Product), _item.UnitOfMeasure, _item.NewValue, _item.FileLineReference);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_null_pending_products()
        {
            // Given / When
            var action = () => new PendingImportItem(_item.Import, null, _item.UnitOfMeasure, _item.NewValue, _item.FileLineReference);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_empty_pending_products()
        {
            // Given / When
            var action = () => new PendingImportItem(_item.Import, Enumerable.Empty<PrimaryProduct>(), _item.UnitOfMeasure, _item.NewValue, _item.FileLineReference);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_update_selected_product_with_null_value()
        {
            // Given / When
            var action = () => _item.MarkSelectedProduct(null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_update_selected_product_and_doesnt_exists_on_pending_products()
        {
            // Given / When
            
            var action = () => _item.MarkSelectedProduct(_primaryProductFaker.Generate());

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_update_selected_product()
        {
            // Given 
            var product = _item.PendingProducts.First().Product;

            // When
            _item.MarkSelectedProduct(product);

            // Then
            _item.SelectedProduct.Id.Should().Be(product.Id);
            _item.SelectedProduct.Should().Be(product);
        }

        [Fact]
        public void Should_create()
        {
            // Given // When
            var result = new PendingImportItem(_item.Import, _item.PendingProducts.Select(x => x.Product), _item.UnitOfMeasure, _item.NewValue, _item.FileLineReference);

            // Then
            result.ImportId.Should().Be(_item.ImportId);
            result.Import.Should().Be(_item.Import);
            result.PendingProducts.Should().NotBeEmpty();
            result.SelectedProductId.Should().BeNull();
            result.SelectedProduct.Should().BeNull();
            result.Status.IsPending().Should().BeTrue();
            result.UnitOfMeasure.Should().Be(_item.UnitOfMeasure);
            result.NewValue.Should().Be(_item.NewValue);
            result.FileLineReference.Should().Be(_item.FileLineReference);
        }
    }
}