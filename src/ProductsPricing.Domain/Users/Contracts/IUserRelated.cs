using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.Users.Contracts
{
    public interface IUserRelated
    {
        User User { get; }
        Guid UserId { get; }
    }
}
