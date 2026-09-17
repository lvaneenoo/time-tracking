using Microsoft.Data.Sqlite;

internal static class SqliteDataReaderExtensions
{
    public static TrackedDate GetDate(this SqliteDataReader reader)
    {
        return new TrackedDate(DateOnly.FromDateTime(reader.GetDateTime(0)));
    }

    public static DateTimeOffset GetModifiedOn(this SqliteDataReader reader) => reader.GetDateTimeOffset(2);
    public static TimeSheetStatus GetStatus(this SqliteDataReader reader) => (TimeSheetStatus)reader.GetInt32(1);

    public static TimeSheetEntry? ToTimeSheetEntry(this SqliteDataReader reader)
    {
        return reader.ToPeriod() is { } period ? new(period, reader.GetComment()) : null;
    }

    private static Comment GetComment(this SqliteDataReader reader) => new(reader.GetString(5));
    private static DateTime GetPeriodEnd(this SqliteDataReader reader) => reader.GetDateTime(4);
    private static DateTime GetPeriodStart(this SqliteDataReader reader) => reader.GetDateTime(3);

    private static bool IsPeriodStartNull(this SqliteDataReader reader) => reader.IsDBNull(3);

    private static Period? ToPeriod(this SqliteDataReader reader)
    {
        return reader.IsPeriodStartNull()
            ? null
            : new(TimeOnly.FromDateTime(reader.GetPeriodStart()), TimeOnly.FromDateTime(reader.GetPeriodEnd()));
    }
}
