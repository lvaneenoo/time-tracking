using Microsoft.Data.Sqlite;

internal class GetTimeSheet : IApplicationQuery
{
    private readonly TrackedDate _date;

    private readonly string _connectionString;

    public GetTimeSheet(IConfiguration configuration, TrackedDate date)
    {
        var connectionString = configuration.GetConnectionString("WriteStore");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException();
        }

        _connectionString = connectionString;
        _date = date;
    }

    public async Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = connection.CreateCommand();

        command.CommandText = RetrieveTimeSheets.ByDate;
        command.Parameters.AddRange(ByTimeSheetDate.Create(_date));

        await connection.OpenAsync(cancellationToken);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var materializer = new TimeSheetResourceMaterializer(reader);

        return await materializer.SingleOrDefaultAsync(cancellationToken) is { } timeSheet
            ? Results.Ok(timeSheet)
            : Results.NotFound();
    }
}
