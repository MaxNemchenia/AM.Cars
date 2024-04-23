using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Domain.Models;
using AM.Cars.Client.Infrustructure.Converters.Interfaces;
using AM.Cars.Client.ViewModels;
using System.Collections.ObjectModel;
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
            var cars = await _apiAdapter.Get();
            _viewModel.Cars = new ObservableCollection<Car>(cars);
        }
        catch (Exception)
        {
        }

        DataContext = _viewModel;
    }
}
