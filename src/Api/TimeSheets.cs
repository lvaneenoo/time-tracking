using Microsoft.Data.Sqlite;

internal class TimeSheets : ITimeSheets
{
    public async Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default)
    {
        using var connection = new SqliteConnection(ConnectionStrings.WriteStore);
        using var command = connection.CreateCommand();

        command.CommandText = SelectTimeSheets.ByDate;

        command.Parameters.AddRange(date.Resolve());

        await connection.OpenAsync(cancellationToken);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var materializer = new TimeSheetMaterializer(reader);

        return await materializer.SingleOrDefaultAsync(cancellationToken);
    }
}
