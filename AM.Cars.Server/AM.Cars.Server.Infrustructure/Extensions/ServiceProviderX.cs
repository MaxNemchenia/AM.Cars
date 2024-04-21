using AM.Cars.Server.Infrustructure.Services.Implementations;
using AM.Cars.Server.Infrustructure.Services.Interfaces;
using AM.Cars.Server.Infrustructure.Utilities.Implementations;
using AM.Cars.Server.Infrustructure.Utilities.Interfaces;
using AM.Cars.Server.Infrustructure.Validators;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AM.Cars.Server.Infrustructure.Extensions;

public static class ServiceProviderX
{
    /// <summary>
    /// Add AutoMapper profiles from the current domain assemblies to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the AutoMapper profiles will be added.</param>
    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        var definedTypes = domainAssemblies
            .Where(a => a.DefinedTypes.Any(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t)))
            .SelectMany(x => x.DefinedTypes).Distinct().ToList();

        var assemblies = definedTypes.Select(x => x.Assembly).Distinct();
        services.AddAutoMapper(assemblies);
    }

    /// <summary>
    /// Add the infrustructure services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the infrustructure services will be added.</param>
    public static void AddInfrustructureServices(this IServiceCollection services)
    {
        services.AddScoped<IImageUtility, ImageUtility>();
    }

    /// <summary>
    /// Add the infrustructure utilities to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the infrustructure services will be added.</param>
    public static void AddInfrustructureUtilities(this IServiceCollection services)
    {
        services.AddScoped<ICarService, CarService>();
    }

    /// <summary>
    /// Add validators to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which validators will be added.</param>
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CarPostDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CarDtoValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
    }
}
