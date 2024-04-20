using AM.Cars.Server.Infrustructure.Database.Builders.Interfaces;

namespace AM.Cars.Server.Infrustructure.Database.Builders.Implementations;

public class ConnectionStringBuilder : IConnectionStringBuilder
{
    /// <inheritdoc/>
    public string BuildConnectionString()
    {
        var server = Environment.GetEnvironmentVariable("MSSQL_SERVER", EnvironmentVariableTarget.Process);
        var user = Environment.GetEnvironmentVariable("MSSQL_USER", EnvironmentVariableTarget.Process);
        var password = Environment.GetEnvironmentVariable("MSSQL_PASSWORD", EnvironmentVariableTarget.Process);
        var database = Environment.GetEnvironmentVariable("MSSQL_DATABASE", EnvironmentVariableTarget.Process);
        var connectionString = $"Server={server};Database={database};User Id={user};Password={password};" +
            "MultipleActiveResultSets=True;TrustServerCertificate=True";

        return connectionString;
    }
}
