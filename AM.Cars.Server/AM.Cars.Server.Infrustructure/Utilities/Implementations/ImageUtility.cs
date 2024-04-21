using AM.Cars.Server.Domain.Constants;
using AM.Cars.Server.Infrustructure.Utilities.Interfaces;

namespace AM.Cars.Server.Infrustructure.Utilities.Implementations;

public class ImageUtility : IImageUtility
{
    private const string extension = ".jpg";

    /// <inheritdoc />
    public async Task<string> SaveImageToVolumeStorageAsync(string imageDataBase64, string imageName)
    {
        var imagePath = GenerateFilePath(imageName);
        var availableFileName = GenerateAvailableFileName(imagePath, extension);

        try
        {
            var imageData = Convert.FromBase64String(imageDataBase64);
            using (var fileStream = new FileStream(availableFileName, FileMode.Create, FileAccess.Write))
            {
                await fileStream.WriteAsync(imageData);
            }

            return availableFileName;
        }
        catch (Exception ex)
        {
            throw new IOException("Failed to save the image to the Docker volume.", ex);
        }
    } 

    /// <inheritdoc />
    public async Task<string> ReadImageFromStorageAsync(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException("Image file not found.");
        }

        var image = await File.ReadAllBytesAsync(imagePath);

        return Convert.ToBase64String(image);
    }

    /// <inheritdoc />
    public void DeleteImageFromStorage(string imagePath)
    {
        File.Delete(imagePath);
    }

    private static string GenerateFilePath(string fileName)
    {
        var volumePath = Environment.GetEnvironmentVariable(EnvironmentConstants.VolumeStorage);

        if (string.IsNullOrEmpty(volumePath))
        {
            throw new InvalidOperationException(
                $"{EnvironmentConstants.VolumeStorage} environment variable is not set.");
        }

        return Path.Combine(volumePath, fileName);
    }

    private static string GenerateAvailableFileName(string filePath, string extension)
    {
        var directory = Path.GetDirectoryName(filePath);
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var newFilePath = Path.Combine(directory, Path.GetFileName(filePath));
        int iterator = 1;

        while (File.Exists(newFilePath))
        {
            var newFileName = $"{fileName}({iterator++}){extension}";
            newFilePath = Path.Combine(directory, newFileName);
        }

        return newFilePath;
    }
}
