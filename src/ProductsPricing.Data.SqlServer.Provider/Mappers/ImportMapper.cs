using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Data.Extensions;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class ImportMapper : IEntityTypeConfiguration<Import>
    {
        public void Configure(EntityTypeBuilder<Import> builder)
        {
            builder.ToTable("Imports");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.Property(x => x.StartedAt).IsRequired();

            builder.Property(x => x.FinishedAt).IsRequired();

            builder.Property(x => x.FileName).HasMaxLength(Import.FILE_NAME_MAX_LENGTH).IsRequired();

            builder.OwnsOne(x => x.Status, x =>
            {
                x.Property(x => x.Name).HasMaxLength(50).IsRequired();
                x.Property(x => x.Value).IsRequired();
            });

            builder.HasMany(x => x.ImpactedProducts).WithOne(x => x.Import);

            builder.HasMany(x => x.Items).WithOne(x => x.Import);

            builder.HasMany(x => x.Logs).WithOne(x => x.Import);
        }
    }
}