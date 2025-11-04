using System.Data.Common;

internal static class DbDataReaderExtensions
{
    public static TimeSheetEntry? ToTimeSheetEntry(this DbDataReader reader) => reader.IsDBNull(2)
        ? null
        : new TimeSheetEntry(new Period(CreateStart(reader), CreateEnd(reader)));

    private static TimeOnly CreateEnd(DbDataReader reader) => TimeOnly.FromDateTime(reader.GetDateTime(3));
    private static TimeOnly CreateStart(DbDataReader reader) => TimeOnly.FromDateTime(reader.GetDateTime(2));
}
