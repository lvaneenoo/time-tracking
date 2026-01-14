using Microsoft.Data.Sqlite;

internal class DeleteTimeSheetEntry(
    ITimeSheets timeSheets,
    WriteStore writeStore,
    TimeSheetEntryId id) : IApplicationCommand
{
    private readonly ITimeSheets _timeSheets = timeSheets;
    private readonly WriteStore _writeStore = writeStore;
    private readonly TimeSheetEntryId _id = id;

    public async Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (await _timeSheets.FindAsync(_id, cancellationToken) is null)
        {
            return Results.NotFound();
        }

        using var command = new SqliteCommand(DeleteTimeSheetEntries.ByTimeSheetEntryId);

        command.Parameters.AddRange(new ByTimeSheetEntryId(_id));

        return await _writeStore.ExecuteNonQueryAsync(command, cancellationToken) switch
        {
            0 => Results.Conflict(),
            1 => Results.NoContent(),
            _ => Results.InternalServerError()
        };
    }
}
