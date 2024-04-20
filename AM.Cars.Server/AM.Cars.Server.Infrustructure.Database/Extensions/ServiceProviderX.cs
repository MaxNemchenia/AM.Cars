using AM.Cars.Server.Application.Repositories;
using AM.Cars.Server.Infrustructure.Database.Builders.Implementations;
using AM.Cars.Server.Infrustructure.Database.Builders.Interfaces;
using AM.Cars.Server.Infrustructure.Database.Context;
using AM.Cars.Server.Infrustructure.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AM.Cars.Server.Infrustructure.Database.Extensions;

public static class ServiceProviderX
{
    /// <summary>
    /// Adds the repositories to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the repositories will be added.</param>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICarRepository, CarRepository>();
    }
    
    /// <summary>
    /// Adds the builders to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the builders will be added.</param>
    public static void AddInfrustructureDatabaseBuilders(this IServiceCollection services)
    {
        services.AddScoped<IConnectionStringBuilder, ConnectionStringBuilder>();
    }

    /// <summary>
    /// Adds the configuration database context to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the database context will be added.</param>
    public static void AddConfigDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ConfigContext>();
    }
}
