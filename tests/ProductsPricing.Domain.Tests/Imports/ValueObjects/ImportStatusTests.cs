using FluentAssertions;
using ProductsPricing.Domain.Imports.ValueObjects;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.ValueObjects
{
    public class ImportStatusTests
    {

        [Fact]
        public void Should_create_running()
        {
            // Given / When
            var importStatus = ImportStatus.Running();

            // Then
            importStatus.IsRunning().Should().BeTrue();
        }

        [Fact]
        public void Should_create_pending()
        {
            // Given / When
            var importStatus = ImportStatus.Pending();

            // Then
            importStatus.IsPending().Should().BeTrue();
        }

        [Fact]
        public void Should_create_failure()
        {
            // Given / When
            var importStatus = ImportStatus.Failure();

            // Then
            importStatus.IsFailure().Should().BeTrue();
        }

        [Fact]
        public void Should_create_success()
        {
            // Given / When
            var importStatus = ImportStatus.Success();

            // Then
            importStatus.IsSuccess().Should().BeTrue();
        }
    }
}
