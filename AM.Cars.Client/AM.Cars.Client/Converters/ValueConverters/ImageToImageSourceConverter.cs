using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace AM.Cars.Client.Converters.ValueConverters;

public class ImageToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Image imageControl)
        {
            return imageControl.Source;
        }

        return default;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
