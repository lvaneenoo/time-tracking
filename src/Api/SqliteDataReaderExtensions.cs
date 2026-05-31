using Microsoft.Data.Sqlite;

internal static class SqliteDataReaderExtensions
{
    public static TimeSheetEntry? ToTimeSheetEntry(this SqliteDataReader reader)
    {
        return reader.IsDBNull(3)
            ? null
            : new TimeSheetEntry(reader.ToPeriod());
    }

    private static Period ToPeriod(this SqliteDataReader reader)
    {
        return new(TimeOnly.FromDateTime(reader.GetDateTime(3)), TimeOnly.FromDateTime(reader.GetDateTime(4)));
    }
}
