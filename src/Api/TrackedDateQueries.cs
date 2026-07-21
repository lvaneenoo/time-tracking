using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;

internal static class TrackedDateQueries
{
    public static bool IsWeekend(this TrackedDate date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }

    public static ReadOnlyCollection<SqliteParameter> Resolve(this TrackedDate date)
    {
        return
        [
            new($"@{TimeSheetTable.Date}", SqliteType.Text)
            {
                Value = date.ToString("yyyy-MM-dd", null)
            }
        ];
    }
}
