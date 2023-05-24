using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.Users.Managers
{
    public interface IUserAccessorManager
    {
        Guid GetCurrentUserId();

        Task<User> GetCurrentUser();
    }
}
