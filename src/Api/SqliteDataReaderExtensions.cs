using Microsoft.Data.Sqlite;

internal static class SqliteDataReaderExtensions
{
    public static TimeSheetEntry? ToTimeSheetEntry(this SqliteDataReader reader) =>
        reader.IsDBNull(2)
            ? null
            : new TimeSheetEntrySnapshot(new TimeSheetEntry(reader.ToPeriod()))
            {
                Id = new TimeSheetEntryId(reader.GetInt64(2))
            };

    private static Period ToPeriod(this SqliteDataReader reader) =>
        new(TimeOnly.FromDateTime(reader.GetDateTime(3)), TimeOnly.FromDateTime(reader.GetDateTime(4)));
}
