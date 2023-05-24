using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class UnitOfMeasureConversionMapper : IEntityTypeConfiguration<UnitOfMeasureConversion>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasureConversion> builder)
        {
            builder.ToTable("UnitOfMeasureConversions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.Property(x => x.ProductsCount).IsRequired();

            builder.Property(x => x.GramsByUnit).HasPrecision(9,4).IsRequired();

            builder.HasOne(x => x.UnitOfMeasure).WithMany();
        }
    }
}
