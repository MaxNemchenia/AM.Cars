using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Commands;
using AM.Cars.Client.Domain.Models;
using AM.Cars.Client.Infrustructure.Converters.Interfaces;
using AM.Cars.Client.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AM.Cars.Client.ViewModels;

public class CarListViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ICarApiAdapter _apiAdapter;

    private readonly IImageConverter _imageConverter;

    private ObservableCollection<Car> _cars;

    private bool _isAllSelected;

    public ObservableCollection<Car> Cars
    {
        get => _cars;
        set
        {
            _cars = value;
            OnPropertyChanged(nameof(Cars));
        }
    }

    public bool IsAllSelected
    {
        get => _isAllSelected;
        set
        {
            _isAllSelected = value;
            OnPropertyChanged(nameof(IsAllSelected));
        }
    }

    public ICollection<long> SelectedCars { get; set; }

    public ICommand CreateCommand { get; private set; }

    public ICommand UpdateCommand { get; private set; }

    public ICommand DeleteCommand { get; private set; }

    public ICommand DeleteCheckedCommand { get; private set; }

    public ICommand ToggleCommand { get; private set; }

    public ICommand ToggleAllCommand { get; private set; }

    public CarListViewModel() { }

    public CarListViewModel(ICarApiAdapter apiAdapter, IImageConverter imageConverter)
    {
        _apiAdapter = apiAdapter;
        _imageConverter = imageConverter;

        CreateCommand = new CreateCommand(CreateCommandExecute);
        UpdateCommand = new UpdateCommand(UpdateCommandExecute);
        DeleteCommand = new DeleteCommand(DeleteCommandExecute);
        DeleteCheckedCommand = new DeleteCheckedCommand(DeleteCheckedCommandExecute);
        ToggleCommand = new ToggleCommand(ToggleCommandExecute);
        ToggleAllCommand = new ToggleAllCommand(ToggleAllCommandExecute);

        SelectedCars = new List<long>();
    }

    public async Task Initialize()
    {
        var cars = await _apiAdapter.GetAsync();
        Cars = new ObservableCollection<Car>(cars);
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void DeleteCommandExecute(object parameter)
    {
        try
        {
            if (parameter is Car selectedCar)
            {
                await _apiAdapter.DeleteAsync(selectedCar.Id);
                await Initialize();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"An error occurred while deleting car: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private async void DeleteCheckedCommandExecute()
    {
        try
        {
            await _apiAdapter.DeleteCheckedAsync(SelectedCars);
            await Initialize();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"An error occurred while deleting car: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void ToggleCommandExecute(object parameter)
    {
        if (parameter is not Car selectedCar)
        {
            return;
        }

        if (SelectedCars.Contains(selectedCar.Id))
        {
            SelectedCars.Remove(selectedCar.Id);
        }
        else
        {
            SelectedCars.Add(selectedCar.Id);
        }

        IsAllSelected = Cars.All(t => t.IsSelected);
    }  

    private void ToggleAllCommandExecute()
    {
        if (IsAllSelected)
        {
            SelectedCars = Cars.Select(t => t.Id).ToList();
            Cars.First().IsSelected = true;
        }
        else
        {
            SelectedCars.Clear();
        }

        foreach (var car in Cars)
        {
            car.IsSelected = IsAllSelected;
        }
    }

    private void CreateCommandExecute()
    {
        ShowSaveForm();
    } 

    private void UpdateCommandExecute(object parameter)
    {
        if (parameter is Car selectedCar)
        {
            ShowSaveForm(selectedCar);
        }
    }

    private void ShowSaveForm(Car selectedCar = default)
    {
        var saveForm = new CarFormView(_apiAdapter, _imageConverter, selectedCar);
        saveForm.Closed += async (s, args) => await CreateWindow_Closed(s, args);
        saveForm.ShowDialog();
    }

    private async Task CreateWindow_Closed(object sender, EventArgs e)
    {
        try
        {
            await Initialize();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"An error occurred while getting car: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
