using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class ImportItemMapper : IEntityTypeConfiguration<ImportItem>
    {
        public void Configure(EntityTypeBuilder<ImportItem> builder)
        {
            builder.ToTable("ImportItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);
        }
    }
}