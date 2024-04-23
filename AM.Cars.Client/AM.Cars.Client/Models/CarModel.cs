using System.ComponentModel;
using System.Windows.Controls;

namespace AM.Cars.Client.Models;

public class CarModel : INotifyPropertyChanged
{
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

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}