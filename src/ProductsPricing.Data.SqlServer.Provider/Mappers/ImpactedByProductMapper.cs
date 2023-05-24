using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Imports.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class ImpactedByProductMapper : IEntityTypeConfiguration<ImpactedByProduct>
    {
        public void Configure(EntityTypeBuilder<ImpactedByProduct> builder)
        {
            builder.HasOne(x => x.RootProduct).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}