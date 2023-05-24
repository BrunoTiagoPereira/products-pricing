using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class PrimaryProductMapper : IEntityTypeConfiguration<PrimaryProduct>
    {
        public void Configure(EntityTypeBuilder<PrimaryProduct> builder)
        {
            builder.HasMany(X => X.NcmProducts).WithOne(x => x.Product);

            builder.HasMany(x => x.UnitOfMeasureConversions).WithOne(x => x.Product).IsRequired(false);
        }
    }
}