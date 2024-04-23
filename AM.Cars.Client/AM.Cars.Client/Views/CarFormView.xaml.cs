using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Domain.Models;
using AM.Cars.Client.Infrustructure.Converters.Interfaces;
using AM.Cars.Client.ViewModels;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AM.Cars.Client.Views;

/// <summary>
/// Interaction logic for CarFormView.xaml
/// </summary>
public partial class CarFormView : Window
{
    private readonly CarFormViewModel _viewModel;

    public CarFormView(ICarApiAdapter adapter, IImageConverter imageConverter, Car car = default)
    {
        _viewModel = new CarFormViewModel(adapter, imageConverter, car);
        DataContext = _viewModel;
        InitializeComponent();
        _viewModel.SaveSuccessful += ViewModel_SaveSuccessful;
    }

    private void ViewModel_SaveSuccessful(object sender, EventArgs e)
    {
        this.Close();
    }

    private void LoadImageButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var imagePath = openFileDialog.FileName;

                var bitmap = new BitmapImage(new Uri(imagePath));
                ImageControl.Source = bitmap;
                _viewModel.CarModel.ImageControl = ImageControl;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"An error occurred while loading the image: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
