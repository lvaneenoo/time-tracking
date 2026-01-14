using System.Data;
using Microsoft.Data.Sqlite;

internal class WriteStore
{
    private readonly string _connectionString;

    public WriteStore(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WriteStore");

        if (connectionString is null || connectionString.Trim() == "")
        {
            throw new InvalidOperationException();
        }

        _connectionString = connectionString;
    }

    public async Task<int> ExecuteNonQueryAsync(SqliteCommand command, CancellationToken cancellationToken = default)
    {
        using var connection = new SqliteConnection(_connectionString);

        command.Connection = connection;

        await connection.OpenAsync(cancellationToken);

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<SqliteDataReader> ExecuteReaderAsync(
        SqliteCommand command,
        CancellationToken cancellationToken = default)
    {
        command.Connection = new SqliteConnection(_connectionString);

        await command.Connection.OpenAsync(cancellationToken);

        return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection, cancellationToken);
    }
}
