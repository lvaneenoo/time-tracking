using Microsoft.Data.Sqlite;

internal static class SqliteDataReaderExtensions
{
    public static TimeSheetEntry ToTimeSheetEntry(this SqliteDataReader reader) =>
        new(new Period(CreateStart(reader), CreateEnd(reader)));

    private static TimeOnly CreateEnd(SqliteDataReader reader) => TimeOnly.FromDateTime(reader.GetDateTime(2));
    private static TimeOnly CreateStart(SqliteDataReader reader) => TimeOnly.FromDateTime(reader.GetDateTime(1));
}
