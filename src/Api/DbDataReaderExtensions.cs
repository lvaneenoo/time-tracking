using System.Data.Common;

internal static class DbDataReaderExtensions
{
    public static TimeSheetEntry? ToTimeSheetEntry(this DbDataReader reader)
    {
        if (reader.IsDBNull(2))
        {
            return null;
        }

        return new TimeSheetEntrySnapshot(new TimeSheetEntry(CreatePeriod(reader)))
        {
            Id = reader.GetInt64(2)
        };
    }

    private static Period CreatePeriod(DbDataReader reader) =>
        new(TimeOnly.FromDateTime(reader.GetDateTime(3)), TimeOnly.FromDateTime(reader.GetDateTime(4)));
}
