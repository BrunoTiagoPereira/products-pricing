using FluentValidation;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsPricing.Core.Communication.Pipelines;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Data.Repositories;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Sintegra.Engines;
using ProductsPricing.Domain.Users.Managers;
using ProductsPricing.Data.SqlServer.Provider;
using ProductsPricing.Application.Commands.Requests;
using ProductsPricing.Domain.Users.Commands.Handlers;
using ProductsPricing.Domain.Users.Commands.Requests;
using ProductsPricing.Domain.Sintegra.Commands.Requests;
using ProductsPricing.Core.Domain.Contracts;
using ProductsPricing.Domain.Sped.Engines;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.ValueObjects;
using ProductsPricing.Core.Data;

namespace ProductsPricing.CrossCutting.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationPipelineBehavior<,>))
                ;

            return services;
        }
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSqlServerDbContext(configuration);

            return services;
        }
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddMediatR( (c) =>
            {
                c.AsScoped();
            }, 
            typeof(ImportSpedFileCommandRequest).Assembly,
            typeof(CreateUserCommandRequest).Assembly, 
            typeof (AddWarningImportLogForSpedItemsWithNoNcmCommandRequest).Assembly);

            services
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<INcmRepository, NcmRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IImportRepository, ImportRepository>()
                .AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>()
                ;

            services
                .AddScoped<IUserAccessorManager, UserAccessorManager>()
                ;

            services
                //.AddScoped<IFileImportEngine<SintegraItem>, SintegraEngine>()
                .AddScoped<IFileImportEngine<SpedItem>, SpedEngine>()
                ;

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblies(new[] { typeof(ImportSpedFileCommandRequest).Assembly, typeof(CreateUserCommandHandler).Assembly, typeof(CreateSintegraProductsFromItemsCommandRequest).Assembly, typeof(AddWarningImportLogForSpedItemsWithNoNcmCommandRequestValidator).Assembly });

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddCoreServices()
                .AddDomainServices()
                .AddDataServices(configuration)
                .AddValidators()
                ;

            return services;
        }
    }
}