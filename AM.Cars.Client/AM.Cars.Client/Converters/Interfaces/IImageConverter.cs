using System.Windows.Controls;

namespace AM.Cars.Client.Infrustructure.Converters.Interfaces;

public interface IImageConverter
{
    /// <summary>
    /// Converts a base64 string to an Image object.
    /// </summary>
    /// <param name="base64String">The base64 representation of the image.</param>
    Image Base64StringToImage(string base64String);

    /// <summary>
    /// Converts an Image object to a base64 string.
    /// </summary>
    /// <param name="imageControl">The Image object to be converted.</param>
    string ImageToBase64String(Image imageControl);
}
