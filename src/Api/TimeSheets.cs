using Microsoft.Data.Sqlite;

internal class TimeSheets(WriteStore writeStore) : ITimeSheets
{
    private readonly WriteStore _writeStore = writeStore;

    public async Task<TimeSheet?> FindAsync(TimeSheetEntryId id)
    {
        using var command = RetrieveTimeSheets.ByTimeSheetEntryId(id);
        using var reader = await _writeStore.ExecuteReaderAsync(command);

        var materializer = new TimeSheetMaterializer(reader);

        return await materializer.SingleOrDefaultAsync();
    }

    public async Task<TimeSheet?> FindAsync(TrackedDate date)
    {
        using var command = new SqliteCommand(RetrieveTimeSheets.ByTimeSheetDate);

        command.Parameters.AddRange(new ByTimeSheetDate(date));

        using var reader = await _writeStore.ExecuteReaderAsync(command);

        var materializer = new TimeSheetMaterializer(reader);

        return await materializer.SingleOrDefaultAsync();
    }
}
