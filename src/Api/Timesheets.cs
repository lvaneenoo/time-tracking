using Microsoft.Data.Sqlite;

internal class Timesheets : ITimesheets
{
    public async Task<Timesheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default)
    {
        using var connection = new SqliteConnection(ConnectionStrings.WriteStore);
        using var command = connection.CreateCommand();

        command.CommandText = SelectTimeSheets.ByDate;

        command.Parameters.AddRange(date.Resolve());

        await connection.OpenAsync(cancellationToken);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var materializer = new TimesheetMaterializer(reader);

        return await materializer.SingleOrDefaultAsync(cancellationToken);
    }
}
