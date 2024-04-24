using AM.Cars.Client.Application.ApiAdapters.Implementations;
using AM.Cars.Client.Application.Extensions;
using AM.Cars.Client.Converters.ValueConverters;
using AM.Cars.Client.Infrustructure.Converters.Implementations;
using AM.Cars.Client.Infrustructure.Converters.Interfaces;
using AM.Cars.Client.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace AM.Cars.Client;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();

        RegisterConverters();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection, configuration);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {

        var mainWindow = _serviceProvider.GetService<CarListView>();
        mainWindow.Show();
    }

    private static void ConfigureServices(ServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CarApiAdapter).Assembly));
        services.AddSingleton<CarListView>();
        services.AddOptions(configuration);
        services.AddDataAdapters();
        services.AddBuilders();
        services.AddTransient<IImageConverter, ImageConverter>();
    }

    private static void RegisterConverters()
    {
        Current.Resources.Add(nameof(Base64ToImageConverter), new Base64ToImageConverter());
        Current.Resources.Add(nameof(ImageToImageSourceConverter), new ImageToImageSourceConverter());
    }
}
