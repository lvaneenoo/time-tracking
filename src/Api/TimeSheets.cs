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

        if (!reader.HasRows)
        {
            return null;
        }

        await reader.ReadAsync();
        var status = (TimeSheetStatus)reader.GetInt32(1);
        var entries = new List<TimeSheetEntry>();

        if (reader.ToTimeSheetEntry() is { } firstEntry)
        {
            entries.Add(firstEntry);
        }

        while (await reader.ReadAsync())
        {
            if (reader.ToTimeSheetEntry() is { } entry)
            {
                entries.Add(entry);
            }
        }

        return new TimeSheet(date, entries, status);
    }
}
