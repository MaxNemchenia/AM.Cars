using AM.Cars.Server.Domain.Entities;

namespace AM.Cars.Server.Application.Repositories;

public interface ICarRepository
{
    /// <summary>
    /// Get a queryable collection of patients.
    /// </summary>
    /// <returns>A queryable collection of patients.</returns>
    IQueryable<Car> Query();

    /// <summary>
    /// Get a car by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the car.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation that retrieves the car.</returns>
    Task<Car?> ReadAsync(long id);

    /// <summary>
    /// Add a new car to the database asynchronously.
    /// </summary>
    /// <param name="car">The car to be added.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation of adding the car.</returns>
    Task AddAsync(Car car);

    /// <summary>
    /// Update an existing car in the database asynchronously.
    /// </summary>
    /// <param name="car">The car to be updated.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation of updating the car.</returns>
    Task UpdateAsync(Car car);

    /// <summary>
    /// Delete an existing car from the database asynchronously using its ID.
    /// </summary>
    /// <param name="id">The ID of the car to be deleted.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation of deleting the car.</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// Delete existing cars from the database asynchronously using their IDs.
    /// </summary>
    /// <param name="ids">The IDs of the cars to be deleted.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation of deleting the cars.</returns>
    Task DeleteAsync(IEnumerable<long> ids);
}