using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.UnitTests.Core.Fakers
{
    public class ProductFake : Product
    {
        public ProductFake(string name, decimal cost, decimal additionalValue, User user) : base(name, cost, additionalValue, user)
        {
        }
    }
}