using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsPricing.Data.SqlServer.Provider.Persistence;

namespace ProductsPricing.Data.SqlServer.Provider
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, DatabaseContext>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString("SqlServerLocal"), op => op.EnableRetryOnFailure(3));
            }, ServiceLifetime.Scoped);

            return services;
        }
    }
}