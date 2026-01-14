using Microsoft.Data.Sqlite;

internal class GetTimeSheet(WriteStore writeStore, TrackedDate date) : IApplicationQuery
{
    private readonly WriteStore _writeStore = writeStore;
    private readonly TrackedDate _date = date;

    public async Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        using var command = new SqliteCommand(RetrieveTimeSheets.ByTimeSheetDate);

        command.Parameters.AddRange(new ByTimeSheetDate(_date));

        using var reader = await _writeStore.ExecuteReaderAsync(command, cancellationToken);

        var materializer = new TimeSheetResourceMaterializer(reader);

        return await materializer.SingleOrDefaultAsync(cancellationToken) is { } timeSheet
            ? Results.Ok(timeSheet)
            : Results.NotFound();
    }
}
