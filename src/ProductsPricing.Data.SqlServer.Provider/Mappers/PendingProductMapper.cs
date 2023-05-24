using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class PendingProductMapper : IEntityTypeConfiguration<PendingProduct>
    {
        public void Configure(EntityTypeBuilder<PendingProduct> builder)
        {
            builder.ToTable("PendingProducts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}