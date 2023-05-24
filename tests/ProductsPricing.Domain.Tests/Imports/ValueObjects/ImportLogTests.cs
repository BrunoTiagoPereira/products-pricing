using FluentAssertions;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.ValueObjects
{
    public class ImportLogTests
    {
        private readonly ImportFaker _importFaker;

        public ImportLogTests()
        {
            _importFaker = new ImportFaker();
        }

        [Fact]
        public void Should_create_information()
        {
            // Given
            var message = "message";

            // When1
            var importLog = new ImportLog(_importFaker.Generate(), LogLevel.Information(), message);

            // Then
            importLog.LogLevel.Should().Be(LogLevel.Information());
            importLog.Message.Should().Be(message);
        }

        [Fact]
        public void Should_create_warning()
        {
            // Given
            var message = "message";

            // When
            var importLog = new ImportLog(_importFaker.Generate(), LogLevel.Warning(), message);

            // Then
            importLog.LogLevel.Should().Be(LogLevel.Warning());
            importLog.Message.Should().Be(message);
        }

        [Fact]
        public void Should_create_error()
        {
            // Given
            var message = "message";

            // When
            var importLog = new ImportLog(_importFaker.Generate(), LogLevel.Error(), message);

            // Then
            importLog.LogLevel.Should().Be(LogLevel.Error());
            importLog.Message.Should().Be(message);
        }
    }
}