using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.Products.Entities
{
    public abstract class Product : AggregateRoot
    {
        public const int MAX_PRODUCT_NAME_LENGTH = 256;
        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public decimal AdditionalValue { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }

        protected Product() { }

        public Product(string name, decimal value, decimal additionalValue, User user)
        {
            UpdateName(name);
            UpdateValue(value);
            UpdateAdditionalValue(additionalValue);
            UpdateUser(user);
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("O nome não pode ser nulo ou vazio");
            }

            if(name.Length > MAX_PRODUCT_NAME_LENGTH)
            {
                throw new DomainException($"O nome deve ter menos de {MAX_PRODUCT_NAME_LENGTH} caracteres.");
            }

            Name = name;
        }

        public void UpdateValue(decimal value)
        {
            if (value < 0)
            {
                throw new DomainException("O valor deve ser maior que 0");
            }

            Value = value;
        }
        public void UpdateAdditionalValue(decimal additionalValue)
        {
            if (additionalValue < 0)
            {
                throw new DomainException("O valor adicional deve ser maior que 0");
            }

            AdditionalValue = additionalValue;
        }

        private void UpdateUser(User user)
        {
            if(user is null)
            {
                throw new DomainException("O usuário não pode ser nulo");
            }

            User = user;
            UserId = user.Id;
        }
    }
}