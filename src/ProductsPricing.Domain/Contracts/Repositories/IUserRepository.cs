using Microsoft.EntityFrameworkCore;
using ProductsPricing.Core.Data;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> EmailIsTakenAsync(string email);

        Task<User?> FindByEmailAsync(string email);
    }
}