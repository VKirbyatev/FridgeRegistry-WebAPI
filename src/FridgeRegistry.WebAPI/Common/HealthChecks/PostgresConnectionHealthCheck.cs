using System.Data.Common;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace FridgeRegistry.WebAPI.Common.HealthChecks;

public class PostgresConnectionHealthCheck : IHealthCheck
{
    private const string DefaultTestQuery = "Select 1";

    public string ConnectionString { get; }

    public string TestQuery { get; }

    public PostgresConnectionHealthCheck(string connectionString, string testQuery = DefaultTestQuery)
    {
        ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        TestQuery = testQuery;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        await using (var connection = new NpgsqlConnection(ConnectionString))
        {
            try
            {
                await connection.OpenAsync(cancellationToken);
                
                var command = connection.CreateCommand();
                command.CommandText = TestQuery;

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            catch (DbException ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }

        return HealthCheckResult.Healthy();
    }
}