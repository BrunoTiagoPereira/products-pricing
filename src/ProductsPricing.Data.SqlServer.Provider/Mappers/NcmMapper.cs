using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Ncms.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class NcmMapper : IEntityTypeConfiguration<Ncm>
    {
        public void Configure(EntityTypeBuilder<Ncm> builder)
        {
            builder.ToTable("Ncms");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.OwnsOne(x => x.Code).Property(x => x.Value).HasMaxLength(8).IsRequired();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(Ncm.MAX_NCM_DESCRIPTION_LENGTH)
                ;

        }
    }
}