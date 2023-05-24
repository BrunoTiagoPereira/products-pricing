using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class RefinedProductMapper : IEntityTypeConfiguration<RefinedProduct>
    {
        public void Configure(EntityTypeBuilder<RefinedProduct> builder)
        {
            
        }
    }
}