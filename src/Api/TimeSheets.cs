using Microsoft.Data.Sqlite;

internal class TimeSheets(WriteStore writeStore) : ITimeSheets
{
    private readonly WriteStore _writeStore = writeStore;

    public async Task<TimeSheet?> FindAsync(TimeSheetEntryId id, CancellationToken cancellationToken = default)
    {
        using var command = RetrieveTimeSheets.ByTimeSheetEntryId(id);
        using var reader = await _writeStore.ExecuteReaderAsync(command, cancellationToken);

        var materializer = new TimeSheetMaterializer(reader);

        return await materializer.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default)
    {
        using var command = new SqliteCommand(RetrieveTimeSheets.ByTimeSheetDate);

        command.Parameters.AddRange(new ByTimeSheetDate(date));

        using var reader = await _writeStore.ExecuteReaderAsync(command, cancellationToken);

        var materializer = new TimeSheetMaterializer(reader);

        return await materializer.SingleOrDefaultAsync(cancellationToken);
    }
}
