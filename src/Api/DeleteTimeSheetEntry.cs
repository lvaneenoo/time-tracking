using Microsoft.Data.Sqlite;

internal class DeleteTimeSheetEntry : IApplicationCommand
{
    private readonly TimeOnly _periodEnd, _periodStart;
    private readonly TrackedDate _date;

    private readonly string _connectionString;

    public DeleteTimeSheetEntry(
        IConfiguration configuration,
        TrackedDate date,
        TimeOnly periodStart,
        TimeOnly periodEnd)
    {
        string? connectionString = configuration.GetConnectionString("WriteStore");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException();
        }

        _connectionString = connectionString;

        _date = date;
        _periodStart = periodStart;
        _periodEnd = periodEnd;
    }

    public async Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (!Period.TryCreate(_periodStart, _periodEnd, out var period))
        {
            return Results.BadRequest();
        }

        using var connection = new SqliteConnection(_connectionString);
        using var command = connection.CreateCommand();

        command.CommandText = DeleteTimeSheetEntries.ByDateAndPeriod;

        command.Parameters.AddRange(_date.Resolve());
        command.Parameters.AddRange(period.Resolve());

        await connection.OpenAsync(cancellationToken);

        return await command.ExecuteNonQueryAsync(cancellationToken) switch
        {
            0 => Results.NotFound(),
            1 => Results.NoContent(),
            _ => Results.InternalServerError()
        };
    }
}
