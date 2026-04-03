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

    public async Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default) =>
        await FindAsync(RetrieveTimeSheets.ByDate, ByTimeSheetDate.Create(date), cancellationToken);

    public async Task<TimeSheet?> FindAsync(TimeSheetEntryId entryId, CancellationToken cancellationToken = default) =>
        await FindAsync(RetrieveTimeSheets.ByEntryId, ByTimeSheetEntryId.Create(entryId), cancellationToken);

    private async Task<TimeSheet?> FindAsync(
        string commandText,
        IEnumerable<SqliteParameter> parameters,
        CancellationToken cancellationToken = default)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = connection.CreateCommand();

        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);

        await connection.OpenAsync(cancellationToken);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var materializer = new TimeSheetMaterializer(reader);

        return await materializer.SingleOrDefaultAsync(cancellationToken);
    }
}
