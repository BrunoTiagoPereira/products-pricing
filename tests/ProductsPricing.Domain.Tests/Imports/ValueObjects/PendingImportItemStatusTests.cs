using FluentAssertions;
using ProductsPricing.Domain.Imports.ValueObjects;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.ValueObjects
{
    public class PendingImportItemStatusTests
    {
        [Fact]
        public void Should_create_pending()
        {
            // Given / When
            var pendingImportStatus = PendingImportItemStatus.Pending();

            // Then
            pendingImportStatus.IsPending().Should().BeTrue();
        }

        [Fact]
        public void Should_create_selected()
        {
            // Given / When
            var pendingImportStatus = PendingImportItemStatus.Selected();

            // Then
            pendingImportStatus.IsSelected().Should().BeTrue();
        }
    }
}
