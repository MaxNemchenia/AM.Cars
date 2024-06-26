﻿using AM.Cars.Server.Infrustructure.Dtos;

namespace AM.Cars.Server.Infrustructure.Services.Interfaces;

public interface ICarService
{
    /// <summary>
    /// Get all cars with their images.
    /// </summary>
    /// <returns>A collection of car DTOs with images.</returns>
    Task<IEnumerable<CarDto>> GetAllAsync();

    /// <summary>
    /// Get a car by its ID with the associated image.
    /// </summary>
    /// <param name="id">The ID of the car to retrieve.</param>
    /// <returns>The car DTO with the associated image.</returns>
    Task<CarDto> GetAsync(long id);

    /// <summary>
    /// Create a new car to the system with the associated image.
    /// </summary>
    /// <param name="CarPostDto">The car DTO to be created.</param>
    Task CreateAsync(CarPostDto carDto);

    /// <summary>
    /// Update an existing car in the system with the associated image.
    /// </summary>
    /// <param name="carDto">The updated car DTO.</param>
    Task UpdateAsync(CarDto carDto);

    /// <summary>
    /// Delete a car from the system and its associated image.
    /// </summary>
    /// <param name="id">The ID of the car to delete.</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// Delete multiple cars from the system and their associated images.
    /// </summary>
    /// <param name="ids">The IDs of the cars to delete.</param>
    Task DeleteAsync(IEnumerable<long> ids);
}
