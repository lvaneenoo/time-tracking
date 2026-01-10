using Microsoft.Data.Sqlite;

internal static class SqliteDataReaderExtensions
{
    private const string TimeFormat = "HH:mm";

    public static TimeSheetEntryResource? ToEntryResource(this SqliteDataReader reader) =>
        reader.IsDBNull(2)
            ? null
            : new TimeSheetEntryResource
            {
                Id = reader.GetInt64(2),
                Period = reader.ToPeriodResource()
            };

    public static TimeSheetEntry? ToTimeSheetEntry(this SqliteDataReader reader) =>
        reader.IsDBNull(2)
            ? null
            : new TimeSheetEntrySnapshot(new TimeSheetEntry(reader.ToPeriod()))
            {
                Id = new TimeSheetEntryId(reader.GetInt64(2))
            };

    private static Period ToPeriod(this SqliteDataReader reader) =>
        new(TimeOnly.FromDateTime(reader.GetDateTime(3)), TimeOnly.FromDateTime(reader.GetDateTime(4)));

    private static PeriodResource ToPeriodResource(this SqliteDataReader reader)
    {
        var start = reader.GetDateTime(3);
        var end = reader.GetDateTime(4);

        return new PeriodResource
        {
            End = end.ToString(TimeFormat),
            Start = start.ToString(TimeFormat)
        };
    }
}
