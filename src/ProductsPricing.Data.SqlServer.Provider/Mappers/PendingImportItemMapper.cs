using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class PendingImportItemMapper : IEntityTypeConfiguration<PendingImportItem>
    {
        public void Configure(EntityTypeBuilder<PendingImportItem> builder)
        {
            builder.OwnsOne(x => x.Status, x =>
            {
                x.Property(y => y.Name).HasMaxLength(50).IsRequired();
                x.Property(y => y.Value).IsRequired();
            });

            builder.HasMany(x => x.PendingProducts).WithOne(x => x.PendingImportItem);

            builder.HasOne(x => x.SelectedProduct).WithMany().HasForeignKey(x => x.SelectedProductId).IsRequired(false);
        }
    }
}