using Microsoft.Data.Sqlite;

internal class TimeSheets : ITimeSheets
{
    private readonly string _connectionString;

    public TimeSheets(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WriteStore");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException();
        }

        _connectionString = connectionString;
    }

    public async Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = connection.CreateCommand();

        command.CommandText = RetrieveTimeSheets.ByDate;

        command.Parameters.AddRange(date.ResolveParameters());

        await connection.OpenAsync(cancellationToken);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var materializer = new TimeSheetMaterializer(reader);

        return await materializer.SingleOrDefaultAsync(cancellationToken);
    }
}
