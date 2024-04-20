using AM.Cars.Server.Infrustructure.Utilities.Interfaces;

namespace AM.Cars.Server.Infrustructure.Utilities.Implementations;

public class ImageUtility : IImageUtility
{
    /// <inheritdoc />
    public async Task<string> SaveImageToVolumeStorageAsync(byte[] imageData, string imageName)
    {
        var imagePath = GenerateFilePath(imageName);

        try
        {
            using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.WriteAsync(imageData);
            }

            return imagePath;
        }
        catch (Exception ex)
        {
            throw new IOException("Failed to save the image to the Docker volume.", ex);
        }
    }

    /// <inheritdoc />
    public Task<byte[]> ReadImageFromStorageAsync(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException("Image file not found.");
        }

        return File.ReadAllBytesAsync(imagePath);
    }

    /// <inheritdoc />
    public void DeleteImageFromStorage(string imagePath)
    {
        File.Delete(imagePath);
    }

    private static string GenerateFilePath(string fileName)
    {
        var volumePath = Environment.GetEnvironmentVariable("VOLUME_PATH");

        if (string.IsNullOrEmpty(volumePath))
        {
            throw new InvalidOperationException("VOLUME_PATH environment variable is not set.");
        }

        return Path.Combine(volumePath, fileName);
    }
}
