using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AM.Cars.Client.Domain.Models;

public class Car : INotifyPropertyChanged
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    private bool _isSelected;

    [JsonIgnore]
    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            _isSelected = value;
            OnPropertyChanged(nameof(IsSelected));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
