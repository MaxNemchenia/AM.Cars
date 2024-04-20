using AM.Cars.Server.Domain.Entities;
using AM.Cars.Server.Infrustructure.Database.Builders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AM.Cars.Server.Infrustructure.Database.Context;

public class ConfigContext : DbContext
{
    private readonly IConnectionStringBuilder _connectionStringBuilder;

    public ConfigContext(
        DbContextOptions<ConfigContext> options,
        IConnectionStringBuilder connectionStringBuilder)
        : base(options)
    {
        _connectionStringBuilder = connectionStringBuilder;
    }

    public DbSet<Car> Cars{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder is { IsConfigured: false })
        {
            optionsBuilder.UseSqlServer(_connectionStringBuilder.BuildConnectionString());
        }
    }
}
