using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class ProductMapper : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.Property(x => x.Name).HasMaxLength(Product.MAX_PRODUCT_NAME_LENGTH).IsRequired();

            builder.Property(x => x.Value).HasPrecision(9,2).IsRequired();

            builder.Property(x => x.AdditionalValue).HasPrecision(9,2).IsRequired().HasDefaultValue(0);
        }
    }
}