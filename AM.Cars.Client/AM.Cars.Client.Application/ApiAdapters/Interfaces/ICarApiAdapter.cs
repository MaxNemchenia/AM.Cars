using AM.Cars.Client.Domain.Models;

namespace AM.Cars.Client.Application.ApiAdapters.Interfaces;

public interface ICarApiAdapter
{
    /// <summary>
    /// Retrieves a list of all cars from the API.
    /// </summary>
    Task<IEnumerable<Car>> GetAsync();

    /// <summary>
    /// Creates a new car by making a request to the API.
    /// </summary>
    /// <param name="car">The data of the new car to be created.</param>
    Task CreateAsync(Car car);

    /// <summary>
    /// Updates the information of a car by making a request to the API.
    /// </summary>
    /// <param name="car">The updated data of the car.</param>
    Task UpdateAsync(Car car);

    /// <summary>
    /// Deletes a car by making a request to the API.
    /// </summary>
    /// <param name="id">The ID of the car to be deleted.</param>
    Task DeleteAsync(long id); 

    /// <summary>
    /// Deletes cars by making a request to the API.
    /// </summary>
    /// <param name="ids">The IDs of the cars to be deleted.</param>
    Task DeleteCheckedAsync(IEnumerable<long> ids);
}