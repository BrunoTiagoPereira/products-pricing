using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.Domain.Users.ValueObjects;

namespace ProductsPricing.Domain.Users.Entities
{
    public class User : AggregateRoot
    {
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        private readonly List<Product> _products;
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private readonly List<Role> _roles;
        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

        private readonly List<Import> _imports;
        public IReadOnlyCollection<Import> Imports => _imports.AsReadOnly();
        protected User()
        {
            _products = new List<Product>();
            _roles = new List<Role>();
            _imports = new List<Import>();
        }


        public User(string email, string password)
        {
            UpdateEmail(email);
            UpdatePassword(password);

            _products = new List<Product>();
            _roles = new List<Role>();
            _imports = new List<Import>();
        }

        public void UpdateEmail(string email)
        {
            Email = new Email(email);
        }

        public void UpdatePassword(string password)
        {
            Password = new Password(password);
        }
        public void AsAdministrator()
        {
            var doesNotHaveAdministratorRoleAlready = !_roles.Any(x => x.IsAdministrator());

            if (doesNotHaveAdministratorRoleAlready)
            {
                _roles.Add(Role.Administrator());
            }
        }
    }
}