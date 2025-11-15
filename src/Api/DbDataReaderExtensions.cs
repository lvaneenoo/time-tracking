using System.Data.Common;

internal static class DbDataReaderExtensions
{
    public static TimeSheetEntry? ToTimeSheetEntry(this DbDataReader reader) => reader.IsDBNull(2)
        ? null
        : new TimeSheetEntrySnapshot(new TimeSheetEntry(CreatePeriod(reader)))
        {
            Id = new TimeSheetEntryId(reader.GetInt64(2))
        };

    private static Period CreatePeriod(DbDataReader reader) =>
        new(TimeOnly.FromDateTime(reader.GetDateTime(3)), TimeOnly.FromDateTime(reader.GetDateTime(4)));
}
