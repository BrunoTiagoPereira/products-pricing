using Microsoft.EntityFrameworkCore;
using ProductsPricing.Core.Data;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public Task<bool> EmailIsTakenAsync(string email)
        {
            return Set.AnyAsync(x => x.Email.Value == email);
        }

        public Task<User?> FindByEmailAsync(string email)
        {
            return Set
                .SingleOrDefaultAsync(x => x.Email.Value == email)
                ;
        }
    }
}