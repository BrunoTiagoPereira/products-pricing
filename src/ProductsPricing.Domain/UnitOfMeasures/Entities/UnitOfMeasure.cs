using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.UnitOfMeasures.Entities
{
    public class UnitOfMeasure : AggregateRoot
    {
        public const int MAX_NAME_LENGTH = 10;
        public UnitOfMeasure(string name, User user)
        {
            UpdateName(name);
            UpdateUser(user);
        }

        protected UnitOfMeasure()
        { }

        public string Name { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        private void UpdateUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            UserId = user.Id;
            User = user;
        }

        private void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name.Length > MAX_NAME_LENGTH)
            {
                throw new DomainException(nameof(name));
            }

            Name = name;
        }
    }
}