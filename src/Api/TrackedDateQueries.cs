using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;

internal static class TrackedDateQueries
{
    private const string TimeSheetDate = "@time_sheet_date";

    public static bool IsWeekend(this TrackedDate date) =>
        date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

    public static ReadOnlyCollection<SqliteParameter> ResolveParameters(this TrackedDate date) =>
        [
            new(TimeSheetDate, SqliteType.Text)
            {
                Value = date.ToString()
            }
        ];
}
