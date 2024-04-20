﻿namespace AM.Cars.Server.Infrustructure.Utilities.Interfaces;

public interface IImageUtility
{
    /// <summary>
    /// Saves the image to the volume storage.
    /// </summary>
    /// <param name="imageData">The byte array representing the image data.</param>
    /// <param name="imageName">The name of the image file.</param>
    /// <returns>The path to the saved image in the volume.</returns>
    Task<string> SaveImageToVolumeStorageAsync(byte[] imageData, string imageName);

    /// <summary>
    /// Reads the image from the storage.
    /// </summary>
    /// <param name="imagePath">The path to the image file in the storage.</param>
    /// <returns>The byte array representing the image data.</returns>
    Task<byte[]> ReadImageFromStorageAsync(string imagePath);

    /// <summary>
    /// Deletes the image from the storage.
    /// </summary>
    /// <param name="imagePath">The path of the image to be deleted.</param>
    void DeleteImageFromStorage(string imagePath);
}
