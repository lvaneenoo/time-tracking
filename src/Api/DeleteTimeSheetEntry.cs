using Microsoft.Data.Sqlite;

internal class DeleteTimeSheetEntry : IApplicationCommand
{
    private readonly TimeSheetEntryId _id;
    private readonly ITimeSheets _timeSheets;

    private readonly string _connectionString;

    public DeleteTimeSheetEntry(IConfiguration configuration, ITimeSheets timeSheets, TimeSheetEntryId id)
    {
        var connectionString = configuration.GetConnectionString("WriteStore");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException();
        }

        _connectionString = connectionString;
        _timeSheets = timeSheets;
        _id = id;
    }

    public async Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (await _timeSheets.FindAsync(_id, cancellationToken) is null)
        {
            return Results.NotFound();
        }

        using var connection = new SqliteConnection(_connectionString);
        using var command = connection.CreateCommand();

        command.CommandText =  DeleteTimeSheetEntries.ById;
        command.Parameters.AddRange(ByTimeSheetEntryId.Create(_id));

        return await command.ExecuteNonQueryAsync(cancellationToken) switch
        {
            0 => Results.Conflict(),
            1 => Results.NoContent(),
            _ => Results.InternalServerError()
        };
    }
}
