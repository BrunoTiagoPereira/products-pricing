using Microsoft.EntityFrameworkCore;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Ncms.Entities;
using ProductsPricing.Domain.Products.Entities;
using ProductsPricing.Domain.UnitOfMeasures.Entities;
using ProductsPricing.Domain.Users.Entities;
using System.Reflection;

namespace ProductsPricing.Data.SqlServer.Provider.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Ncm> Ncms { get; private set; }
        public DbSet<NcmProduct> NcmProducts { get; private set; }
        public DbSet<Ingredient> Ingredients { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<PrimaryProduct> PrimaryProducts { get; private set; }
        public DbSet<RefinedProduct> RefinedProducts { get; private set; }
        public DbSet<UnitOfMeasureConversion> UnitOfMeasureConversions { get; private set; }
        public DbSet<Import> Imports { get; private set; }
        public DbSet<ImportItem> ImportItems { get; private set; }
        public DbSet<EvaluatedImportItem> EvaluatedImportItems { get; private set; }
        public DbSet<PendingImportItem> PendingImportItems { get; private set; }
        public DbSet<ProcessedImportItem> ProcessedImportItems { get; private set; }
        public DbSet<ImpactedProduct> ImpactedProducts { get; private set; }
        public DbSet<PendingProduct> PendingProducts { get; private set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; private set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}