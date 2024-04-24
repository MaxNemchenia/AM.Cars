﻿using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Commands;
using AM.Cars.Client.Domain.Models;
using AM.Cars.Client.Infrustructure.Converters.Interfaces;
using AM.Cars.Client.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AM.Cars.Client.ViewModels;

public class CarFormViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public event EventHandler SaveSuccessful;

    private readonly ICarApiAdapter _apiAdapter;

    private readonly IImageConverter _imageConverter;

    private CarModel _carModel;

    public CarModel CarModel
    {
        get { return _carModel; }
        set
        {
            _carModel = value;
            OnPropertyChanged(nameof(CarModel));
        }
    }

    public ICommand SaveCommand { get; private set; }

    public CarFormViewModel(
        ICarApiAdapter apiAdapter,
        IImageConverter imageConverter,
        Car car = default)
    {
        _apiAdapter = apiAdapter;
        _imageConverter = imageConverter;

        SaveCommand = new SaveCommand(SaveCommandExecute);
        CarModel = car == default ? new CarModel() : CreateCarModel(car);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnSaveSuccessful()
    {
        SaveSuccessful?.Invoke(this, EventArgs.Empty);
    }

    private async Task SaveCommandExecute(object parameter)
    {
        if (parameter is not CarModel carModel)
        {
            return;
        }

        try
        {
            var car = new Car()
            {
                Id = carModel.Id,
                Name = carModel.Name,
                Image = _imageConverter.ImageToBase64String(carModel.ImageControl),
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

    private CarModel CreateCarModel(Car car)
        => new()
        {
            Id = car.Id,
            Name = car.Name,
            ImageControl = _imageConverter.Base64StringToImage(car.Image),
        };
}