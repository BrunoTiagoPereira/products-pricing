using Bogus;
using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using Xunit;

namespace ProductsPricing.Domain.Tests.Products.Entities
{
    public class ProductTests
    {

        private readonly Faker _faker;
        private readonly UserFaker _userFaker;

        public ProductTests()
        {
            _userFaker = new UserFaker();   
            _faker = new Faker();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_throw_when_creating_with_invalid_name(string name)
        {
            // Given / When
            var action = () => new ProductFake(name, 0, 0, _userFaker.Generate());

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_name_exceeds_max_length()
        {
            // Given / When
            var action = () => new ProductFake(_faker.Lorem.Letter(Product.MAX_PRODUCT_NAME_LENGTH + 1), 0, 0, _userFaker.Generate());

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_Value_is_less_than_0()
        {
            // Given / When
            var action = () => new ProductFake(_faker.Lorem.Letter(Product.MAX_PRODUCT_NAME_LENGTH), -1, 0, _userFaker.Generate());

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_additional_Value_is_less_than_0()
        {
            // Given / When
            var action = () => new ProductFake(_faker.Lorem.Letter(Product.MAX_PRODUCT_NAME_LENGTH), 0, -1, _userFaker.Generate());

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_null_user()
        {
            // Given / When
            var action = () => new ProductFake(_faker.Lorem.Letter(Product.MAX_PRODUCT_NAME_LENGTH), 0, 1, null);

            // Thens
            action.Should().Throw<DomainException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_throw_when_updating_with_invalid_name(string name)
        {
            // Given / When
            var action = () => GetProduct().UpdateName(name);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_updating_name_and_exceeds_max_length()
        {
            // Given / When
            var action = () => GetProduct().UpdateName(_faker.Lorem.Letter(Product.MAX_PRODUCT_NAME_LENGTH + 1));

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_updating_and_Value_is_less_than_0()
        {
            // Given / When
            var action = () => GetProduct().UpdateValue(-1);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_updating_and_additional_Value_is_less_than_0()
        {
            // Given / When
            var action = () => GetProduct().UpdateAdditionalValue(-1);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_create_product()
        {
            // Given
            var name = _faker.Commerce.Product();
            var Value = 1;
            var additionalValue = 2;
            var user = _userFaker.Generate();

            // When
            var product = new ProductFake(name, Value, additionalValue, user);

            // Then
            product.Name.Should().Be(name);
            product.Value.Should().Be(Value);
            product.AdditionalValue.Should().Be(additionalValue);
            product.User.Should().Be(user);
            product.UserId.Should().Be(user.Id);
        }

        private ProductFake GetProduct() => new (_faker.Commerce.Product(), 0, 0, _userFaker.Generate());


    }
}