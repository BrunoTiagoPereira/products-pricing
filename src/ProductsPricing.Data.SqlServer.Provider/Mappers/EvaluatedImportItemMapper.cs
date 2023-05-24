using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class EvaluatedImportItemMapper : IEntityTypeConfiguration<EvaluatedImportItem>
    {
        public void Configure(EntityTypeBuilder<EvaluatedImportItem> builder)
        {
            builder.Property(x => x.NewValue).HasPrecision(9,2).IsRequired();

            builder.HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.NoAction);

        }
    }
}