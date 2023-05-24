using Bogus;
using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Ncms.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using Xunit;

namespace ProductsPricing.Domain.Tests.Ncms.Entities
{
    public class NcmTests
    {
        private readonly Faker _faker;
        private readonly NcmFaker _ncmFaker;

        public NcmTests()
        {
            _faker = new Faker();
            _ncmFaker = new NcmFaker();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_throw_when_invalid_null_or_empty_description(string description)
        {
            // Given / When
            var ncm = _ncmFaker.Generate();
            var action = () => new Ncm(ncm.Code.Value, description);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_description_exceeds_max_characters()
        {
            // Given / When
            var ncm = _ncmFaker.Generate();
            var description = _faker.Lorem.Letter(Ncm.MAX_NCM_DESCRIPTION_LENGTH + 1);
            var action = () => new Ncm(ncm.Code.Value, description);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_create()
        {
            // Given
            var ncm = _ncmFaker.Generate();

            // When
            var result = new Ncm(ncm.Code.Value, ncm.Description);

            // Then
            result.Code.Value.Should().Be(ncm.Code.Value);
            result.Description.Should().Be(ncm.Description);
        }
    }
}