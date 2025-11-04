using Microsoft.Data.Sqlite;

internal class TimeSheets : ITimeSheets
{
    private readonly string _connectionString;

    public TimeSheets(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WriteStore");

        if (connectionString is null || connectionString.Trim() == "")
        {
            throw new InvalidOperationException();
        }

        _connectionString = connectionString;
    }

    public async Task<TimeSheet?> FindAsync(TrackedDate date)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = new SqliteCommand(WriteStore.Instance.FetchTimeSheetsByDate, connection);

        command.Parameters.Add(new SqliteParameter("@time_sheet_date", (DateOnly)date));

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var timeSheets = TimeSheetMaterializer.Materialize(reader);

        return await timeSheets.SingleOrDefaultAsync();
    }
}
