using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class ImpactedProductMapper : IEntityTypeConfiguration<ImpactedProduct>
    {
        public void Configure(EntityTypeBuilder<ImpactedProduct> builder)
        {
            builder.ToTable("ImpactedProducts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.OwnsOne(x => x.Status, x =>
            {
                x.Property(y => y.Name).HasMaxLength(50).IsRequired();
                x.Property(y => y.Value).IsRequired();
            });

            builder.HasOne(x => x.TargetProduct).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}