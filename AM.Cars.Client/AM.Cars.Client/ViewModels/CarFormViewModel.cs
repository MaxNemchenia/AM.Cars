using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Commands;
using AM.Cars.Client.Domain.Models;
using AM.Cars.Client.Infrustructure.Converters.Interfaces;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AM.Cars.Client.ViewModels;

public class CarFormViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public event EventHandler SaveSuccessful;

    private readonly ICarApiAdapter _apiAdapter;

    private readonly IImageConverter _imageConverter;

    private string _name;

    private Image _imageControl;

    public long Id { get; set; }

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public Image ImageControl
    {
        get { return _imageControl; }
        set
        {
            _imageControl = value;
            OnPropertyChanged(nameof(ImageControl));
        }
    }

    public ICommand SaveCommand { get; private set; }

    public CarFormViewModel(
        ICarApiAdapter apiAdapter,
        IImageConverter imageConverter,
        CarViewModel car = default)
    {
        _apiAdapter = apiAdapter;
        _imageConverter = imageConverter;

        SaveCommand = new SaveCommand(SaveCommandExecute);
        Initialize(car);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnSaveSuccessful()
    {
        SaveSuccessful?.Invoke(this, EventArgs.Empty);
    }

    private async Task SaveCommandExecute()
    {
        try
        {
            var car = new Car()
            {
                Id = Id,
                Name = Name,
                Image = _imageConverter.ImageToBase64String(ImageControl),
            };

            if (car.Id == 0)
            {
                await _apiAdapter.CreateAsync(car);
            }
            else
            {
                await _apiAdapter.UpdateAsync(car);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"An error occurred while save car: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        OnSaveSuccessful();
    }

    private void Initialize(CarViewModel carViewModel)
    {
        if (carViewModel == default)
        {
            return;
        }

        Id = carViewModel.Car.Id;
        Name = carViewModel.Car.Name;
        ImageControl = _imageConverter.Base64StringToImage(carViewModel.Car.Image); 
    }
}