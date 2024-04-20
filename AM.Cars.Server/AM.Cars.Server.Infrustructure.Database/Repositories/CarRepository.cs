using AM.Cars.Server.Application.Repositories;
using AM.Cars.Server.Domain.Entities;
using AM.Cars.Server.Infrustructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace AM.Cars.Server.Infrustructure.Database.Repositories;

public class CarRepository : ICarRepository
{
    private readonly ConfigContext _dbContext;

    public CarRepository(ConfigContext dbContext)
        => _dbContext = dbContext;

    /// <inheritdoc/>
    public IQueryable<Car> Query()
        => _dbContext.Cars.AsNoTracking().AsQueryable();

    /// <inheritdoc/>
    public Task<Car?> ReadAsync(long id)
        => Query().FirstOrDefaultAsync(c => c.Id == id); 

    /// <inheritdoc/>
    public IQueryable<Car> QueryAsNoTracking()
        => _dbContext.Cars.AsNoTracking().AsQueryable();

    /// <inheritdoc/>
    public Task<Car?> ReadAsNoTrackingAsync(long id)
        => QueryAsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

    /// <inheritdoc/>
    public async Task AddAsync(Car car)
    {
        await _dbContext.Cars.AddAsync(car);
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Car car)
    {
        _dbContext.Cars.Update(car);
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Car car)
    {
        _dbContext.Cars.Remove(car);
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(IEnumerable<Car> cars)
    {
        _dbContext.Cars.RemoveRange(cars);
        await _dbContext.SaveChangesAsync();
    }
}
