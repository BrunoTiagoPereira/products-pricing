using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class ProcessedImportItemMapper : IEntityTypeConfiguration<ProcessedImportItem>
    {
        public void Configure(EntityTypeBuilder<ProcessedImportItem> builder)
        {
            builder.HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}