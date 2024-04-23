using AM.Cars.Client.Infrustructure.Converters.Interfaces;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AM.Cars.Client.Infrustructure.Converters.Implementations;

public class ImageConverter : IImageConverter
{
    public Image Base64StringToImage(string base64String)
    {
        var binaryData = Convert.FromBase64String(base64String);

        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = new MemoryStream(binaryData);
        bitmapImage.EndInit();

        var imageControl = new Image
        {
            Source = bitmapImage
        };

        return imageControl;
    }

    public string ImageToBase64String(Image imageControl)
    {
        var bitmapSource = (BitmapSource)imageControl.Source;
        byte[] imageData;

        using var memoryStream = new MemoryStream();
        var encoder = new JpegBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        encoder.Save(memoryStream);
        imageData = memoryStream.ToArray();

        return Convert.ToBase64String(imageData);
    }
}
