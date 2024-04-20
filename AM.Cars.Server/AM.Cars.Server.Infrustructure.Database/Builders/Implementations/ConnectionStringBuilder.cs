using AM.Cars.Server.Infrustructure.Database.Builders.Interfaces;
using static AM.Cars.Server.Domain.Constants.EnvironmentConstants;

namespace AM.Cars.Server.Infrustructure.Database.Builders.Implementations;

public class ConnectionStringBuilder : IConnectionStringBuilder
{
    /// <inheritdoc/>
    public string BuildConnectionString()
    {
        var server = Environment.GetEnvironmentVariable(DbServer, EnvironmentVariableTarget.Process);
        var user = Environment.GetEnvironmentVariable(DbUser, EnvironmentVariableTarget.Process);
        var password = Environment.GetEnvironmentVariable(DbPassword, EnvironmentVariableTarget.Process);
        var database = Environment.GetEnvironmentVariable(DbName, EnvironmentVariableTarget.Process);
        var connectionString = $"Server={server};Database={database};User Id={user};Password={password};" +
            "MultipleActiveResultSets=True;TrustServerCertificate=True";

        return connectionString;
    }
}
