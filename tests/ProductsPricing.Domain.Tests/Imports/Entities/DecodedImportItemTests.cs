using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.Entities
{
    public class DecodedImportItemTests
    {
        private readonly DecodedImportItemFaker _decodedImportItemFaker;
        private readonly DecodedImportItem _item;

        public DecodedImportItemTests()
        {
            _decodedImportItemFaker = new DecodedImportItemFaker();
            _item = _decodedImportItemFaker.Generate();
        }

        [Fact]
        public void Should_throw_when_invalid_unit_of_measure()
        {
            // Given / When
            var action = () => new DecodedImportItemFake(_item.Import, null, _item.NewValue, _item.FileLineReference);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_throw_when_invalid_new_value(decimal value)
        {
            // Given / When
            var action = () => new DecodedImportItemFake(_item.Import, _item.UnitOfMeasure, value, _item.FileLineReference);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_invalid_file_line_reference()
        {
            // Given / When
            var action = () => new DecodedImportItemFake(_item.Import, _item.UnitOfMeasure, _item.NewValue, 0);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_create()
        {
            // Given // When
            var result = new DecodedImportItemFake(_item.Import, _item.UnitOfMeasure, _item.NewValue, _item.FileLineReference);

            // Then
            result.ImportId.Should().Be(_item.ImportId);
            result.Import.Should().Be(_item.Import);
            result.UnitOfMeasure.Should().Be(_item.UnitOfMeasure);
            result.NewValue.Should().Be(_item.NewValue);
            result.FileLineReference.Should().Be(_item.FileLineReference);
        }
    }
}