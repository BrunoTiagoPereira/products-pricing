using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsPricing.Domain.Products.Entities;

namespace ProductsPricing.Data.SqlServer.Provider.Mappers
{
    public class IngredientMapper : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("Ingredients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Ignore(x => x.Events);

            builder.HasOne(x => x.RootProduct).WithMany(x => x.Ingredients).OnDelete(DeleteBehavior.NoAction);
        }
    }
}