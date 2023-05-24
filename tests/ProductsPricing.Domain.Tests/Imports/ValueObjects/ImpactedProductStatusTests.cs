using FluentAssertions;
using ProductsPricing.Domain.Imports.ValueObjects;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.ValueObjects
{
    public class ImpactedProductStatusTests
    {
        [Fact]
        public void Should_create_pending()
        {
            // Given / When
            var pendingImportStatus = ImpactedProductStatus.Pending();

            // Then
            pendingImportStatus.IsPending().Should().BeTrue();
        }

        [Fact]
        public void Should_create_evaluated()
        {
            // Given / When
            var pendingImportStatus = ImpactedProductStatus.Recalculated();

            // Then
            pendingImportStatus.IsRecalculated().Should().BeTrue();
        }
    }
}
