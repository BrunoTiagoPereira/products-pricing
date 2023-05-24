using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class UnitOfMeasureMapper : IEntityTypeConfiguration<UnitOfMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            builder.ToTable("UnitOfMeasures");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.Property(x => x.Name).HasMaxLength(UnitOfMeasure.MAX_NAME_LENGTH).IsRequired();

            builder.HasOne(x => x.User).WithMany();
        }
    }
}