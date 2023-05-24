using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class DecodedImportItemMapper : IEntityTypeConfiguration<DecodedImportItem>
    {
        public void Configure(EntityTypeBuilder<DecodedImportItem> builder)
        {
            builder.Property(x => x.NewValue).HasPrecision(9, 2).IsRequired();

            builder.Property(x => x.FileLineReference).IsRequired();

            builder.HasOne(x => x.UnitOfMeasure).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}