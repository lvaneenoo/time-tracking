using Microsoft.Data.Sqlite;

internal class DeleteTimeSheetEntry(TrackedDate date, Period period) : IStorageCommand
{
    private readonly Period _period = period;
    private readonly TrackedDate _date = date;

    public async Task<int> ExecuteAsync(CancellationToken cancellationToken)
    {
        using var connection = new SqliteConnection(ConnectionStrings.WriteStore);
        using var command = connection.CreateCommand();

        command.CommandText = DeleteTimeSheetEntries.ByDateAndPeriod;

        command.Parameters.AddRange(_date.Resolve());
        command.Parameters.AddRange(_period.Resolve());

        await connection.OpenAsync(cancellationToken);

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
