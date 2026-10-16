using Microsoft.Data.Sqlite;

internal class InsertTimeSheetEntry(TrackedDate date, Period period, Comment comment) : IStorageCommand
{
    private static readonly string Insert = @"
        INSERT INTO time_sheet_entries
                    (
                        time_sheet_date,
                        period_start,
                        period_end,
                        comment
                    )
             VALUES (
                        @time_sheet_date,
                        @period_start,
                        @period_end,
                        @comment
                    )
        ";

    private readonly Comment _comment = comment;
    private readonly Period _period = period;
    private readonly TrackedDate _date = date;

    public async Task<int> ExecuteAsync(CancellationToken cancellationToken)
    {
        using var connection = new SqliteConnection(ConnectionStrings.WriteStore);
        using var command = connection.CreateCommand();

        command.CommandText = Insert;

        command.Parameters.AddRange(_date.Resolve());
        command.Parameters.AddRange(_period.Resolve());
        command.Parameters.AddRange(_comment.Resolve());

        await connection.OpenAsync(cancellationToken);

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
