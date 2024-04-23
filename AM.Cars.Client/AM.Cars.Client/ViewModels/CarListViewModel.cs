using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Commands;
using AM.Cars.Client.Domain.Models;
using AM.Cars.Client.Infrustructure.Converters.Interfaces;
using AM.Cars.Client.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace AM.Cars.Client.ViewModels;

public class CarListViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ICarApiAdapter _apiAdapter;

    private readonly IImageConverter _imageConverter;

    private ObservableCollection<Car> _cars;

    public ObservableCollection<Car> Cars
    {
        get => _cars;
        set
        {
            _cars = value;
            OnPropertyChanged(nameof(Cars));
        }
    }

    public ICommand CreateCommand { get; private set; }

    public ICommand UpdateCommand { get; private set; }

    public ICommand DeleteCommand { get; private set; }

    public CarListViewModel() { }

    public CarListViewModel(ICarApiAdapter apiAdapter, IImageConverter imageConverter)
    {
        _apiAdapter = apiAdapter;
        _imageConverter = imageConverter;

        _selectedCars = new ObservableCollection<Car>();

        CreateCommand = new CreateCommand(CreateCommandExecute);
        UpdateCommand = new UpdateCommand(UpdateCommandExecute);
        DeleteCommand = new DeleteCommand(DeleteCommandExecute);
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
                await _apiAdapter.Delete(selectedCar.Id).ConfigureAwait(true);

                var cars = await _apiAdapter.Get().ConfigureAwait(true);
                Cars = new ObservableCollection<Car>(cars);
            }
        }
        catch (Exception)
        {
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
            var cars = await _apiAdapter.Get().ConfigureAwait(true);
            Cars = new ObservableCollection<Car>(cars);
        }
        catch (Exception)
        {
        }
    }
}
