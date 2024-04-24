using AM.Cars.Client.Application.ApiAdapters.Implementations;
using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Application.Builders.Implementations;
using AM.Cars.Client.Application.Builders.Interfaces;
using AM.Cars.Client.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AM.Cars.Client.Application.Extensions;

public static class ServiceProviderX
{
    public static void AddDataAdapters(this IServiceCollection services)
    {
        services.AddScoped<ICarApiAdapter, CarApiAdapter>();
    } 

    public static void AddBuilders(this IServiceCollection services)
    {
        services.AddScoped<IHttpMessageBuilder, HttpMessageBuilder>();
    } 

    public static void AddOptions(this IServiceCollection services, IConfiguration configuration )
    {
        services.Configure<ApiOptions>(configuration.GetSection(nameof(ApiOptions)));
    }
}
