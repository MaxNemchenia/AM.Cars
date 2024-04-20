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
        => _dbContext.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

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
    public async Task DeleteAsync(long id)
    {
        var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);

        if (car != null)
        {
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var cars = _dbContext.Cars.Where(c => ids.Contains(c.Id));
        _dbContext.Cars.RemoveRange(cars);
        await _dbContext.SaveChangesAsync();
    }
}
