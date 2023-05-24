using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.Users.Managers
{
    public interface IUserManager
    {
        string GenerateToken(User user);
    }
}