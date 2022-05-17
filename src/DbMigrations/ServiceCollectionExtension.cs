using DbMigrations.Options;
using DbMigrations.Services.DbOperationServices;
using DbMigrations.Services.MigrationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DbMigrations;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMigrationService<TDbMigrationService, TContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbMigrationService : DbOperationBase<TContext>
        where TContext : DbContext
        => services.AddDbOperationService<TDbMigrationService, TContext>(configuration)
                   .AddTransient<IMigrationService, MigrationService>();

    public static IServiceCollection AddDbOperationService<TDbMigrationService, TContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbMigrationService : DbOperationBase<TContext>
        where TContext : DbContext
        => services.AddTransient<IDbOperationService, TDbMigrationService>()
                   .Configure<DbMigraionOptions>(configuration.GetSection(DbMigraionOptions.SectionName));
}