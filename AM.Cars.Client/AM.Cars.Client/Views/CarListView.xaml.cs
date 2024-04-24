using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Infrustructure.Converters.Interfaces;
using AM.Cars.Client.ViewModels;
using System.Windows;

namespace AM.Cars.Client.Views;

/// <summary>
/// Interaction logic for CarListView.xaml
/// </summary>
public partial class CarListView : Window
{
    private readonly CarListViewModel _viewModel;

    private readonly ICarApiAdapter _apiAdapter;

    public CarListView()
    {
    }

    public CarListView(ICarApiAdapter carApiAdapter, IImageConverter imageConverter)
    {
        _apiAdapter = carApiAdapter;
        _viewModel = new CarListViewModel(_apiAdapter, imageConverter);
        InitializeComponent();
        Loaded += CarListView_Loaded;
    }

    private async void CarListView_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            await _viewModel.Initialize();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"An error occurred while loading the page: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        DataContext = _viewModel;
    }
}
