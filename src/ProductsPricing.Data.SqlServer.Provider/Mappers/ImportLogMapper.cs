using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class ImportLogMapper : IEntityTypeConfiguration<ImportLog>
    {
        public void Configure(EntityTypeBuilder<ImportLog> builder)
        {
            builder.ToTable("ImportLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.Property(x => x.Message).HasMaxLength(10000).IsRequired();

            builder.OwnsOne(x => x.LogLevel, (x) =>
            {
                x.Property(x => x.Name).HasMaxLength(50).IsRequired();
                x.Property(x => x.Value).IsRequired();
            });
        }
    }
}