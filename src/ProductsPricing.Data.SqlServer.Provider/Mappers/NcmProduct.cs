using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Ncms.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class NcmProductMapper : IEntityTypeConfiguration<NcmProduct>
    {
        public void Configure(EntityTypeBuilder<NcmProduct> builder)
        {
            builder.ToTable("NcmProducts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

        }
    }
}