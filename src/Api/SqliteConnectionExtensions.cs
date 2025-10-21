using Microsoft.Data.Sqlite;

internal static class SqliteConnectionExtensions
{
    public static async IAsyncEnumerable<TimeSheet> RetrieveTimeSheets(this SqliteConnection connection, TrackedDate date)
    {
        using var command = new SqliteCommand(WriteStore.FetchTimeSheetsByDate, connection);

        command.Parameters.Add(new SqliteParameter("@time_sheet_date", (DateOnly)date));

        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows)
        {
            yield break;
        }

        var entries = new List<TimeSheetEntry>();

        while (await reader.ReadAsync())
        {
            entries.Add(reader.ToTimeSheetEntry());
        }

        yield return new TimeSheet(date, entries);
    }
}
