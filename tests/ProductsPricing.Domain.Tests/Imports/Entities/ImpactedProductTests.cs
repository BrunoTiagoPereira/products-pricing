using FluentAssertions;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.Entities
{
    public class ImpactedProductTests
    {
        private readonly ImpactedProductFaker _impactedProductFaker;
        private readonly ImpactedProduct _item;

        public ImpactedProductTests()
        {
            _impactedProductFaker = new ImpactedProductFaker();
            _item = _impactedProductFaker.Generate();
        }

        [Fact]
        public void Should_throw_when_invalid_import()
        {
            // Given / When
            var action = () => new ImpactedProduct(null, _item.TargetProduct);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_invalid_target_product()
        {
            // Given / When
            var action = () => new ImpactedProduct(_item.Import, null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_mark_as_recalculated()
        {
            // Given / When
            _item.MarkAsRecalculated();

            // Then
            _item.Status.IsRecalculated().Should().BeTrue();
        }

        [Fact]
        public void Should_create()
        {
            // Given // When
            var result = new ImpactedProduct(_item.Import, _item.TargetProduct);

            // Then
            result.ImportId.Should().Be(_item.ImportId);
            result.Import.Should().Be(_item.Import);
            result.TargetProductId.Should().Be(_item.TargetProductId);
            result.TargetProduct.Should().Be(_item.TargetProduct);
        }
    }
}