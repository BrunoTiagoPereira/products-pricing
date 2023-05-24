using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Data.Extensions;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.OwnsOne(x => x.Email).Property(x => x.Value).HasMaxLength(300).IsRequired();

            builder.OwnsOne(x => x.Password).Property(x => x.Hash).HasMaxLength(150).IsRequired();

            builder.Property(x => x.Roles).IsRequired(false);

            builder.Property(x => x.Roles).HasMaxLength(10000).HasJsonConversion();

            builder.HasMany(x => x.Products).WithOne(x => x.User);

            builder.HasMany(x => x.Imports).WithOne(x => x.User);
        }
    }
}