using Bogus;
using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Ncms.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using Xunit;

namespace ProductsPricing.Domain.Tests.Ncms.Entities
{
    public class NcmProductTests
    {
        private readonly Faker _faker;
        private readonly NcmFaker _ncmFaker;
        private readonly PrimaryProductFaker _primaryProductFaker;

        public NcmProductTests()
        {
            _faker = new Faker();
            _ncmFaker = new NcmFaker();
            _primaryProductFaker = new PrimaryProductFaker();
        }


        [Fact]
        public void Should_throw_when_null_ncm()
        {
            // Given / When
            var ncm = _ncmFaker.Generate();
            var product = _primaryProductFaker.Generate();
            var action = () => new NcmProduct(null, product);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_null_product()
        {
            // Given / When
            var ncm = _ncmFaker.Generate();
            var action = () => new NcmProduct(ncm, null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_create()
        {
            // Given
            var ncm = _ncmFaker.Generate();
            var primaryProduct = _primaryProductFaker.Generate();

            // When
            var result = new NcmProduct(ncm, primaryProduct);

            // Then
            result.Ncm.Should().Be(ncm);
            result.NcmId.Should().Be(ncm.Id);
            result.Product.Should().Be(primaryProduct);
            result.ProductId.Should().Be(primaryProduct.Id);
        }
    }
}