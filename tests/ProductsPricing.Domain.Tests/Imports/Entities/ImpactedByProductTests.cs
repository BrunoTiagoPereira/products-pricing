using FluentAssertions;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.Entities
{
    public class ImpactedByProductTests
    {
        private readonly ImpactedByProductFaker _impactedByProductFaker;
        private readonly ImpactedByProduct _item;

        public ImpactedByProductTests()
        {
            _impactedByProductFaker = new ImpactedByProductFaker();
            _item = _impactedByProductFaker.Generate();
        }
        [Fact]
        public void Should_throw_when_invalid_root_product()
        {
            // Given / When
            var action = () => new ImpactedByProduct(_item.Import, null, _item.TargetProduct);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_create()
        {
            // Given // When
            var result = new ImpactedByProduct(_item.Import, _item.RootProduct, _item.TargetProduct);

            // Then
            result.RootProductId.Should().Be(_item.RootProductId);
            result.RootProduct.Should().Be(_item.RootProduct);
        }
    }
}