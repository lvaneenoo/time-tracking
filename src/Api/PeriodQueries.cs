using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;

internal static class PeriodQueries
{
    private const string TimeOfDay = "HH:mm";

    public static bool Overlaps(this Period period, Period other) => period.StartsIn(other) || period.EndsIn(other);

    public static ReadOnlyCollection<SqliteParameter> Resolve(this Period period)
    {
        return
        [
            new SqliteParameter($"@{TimeSheetEntryTable.PeriodStart}", SqliteType.Text)
            {
                Value = period.Start.ToString(TimeOfDay)
            },
            new SqliteParameter($"@{TimeSheetEntryTable.PeriodEnd}", SqliteType.Text)
            {
                Value = period.End.ToString(TimeOfDay)
            }
        ];
    }

    public static PeriodResource ToResource(this Period period)
    {
        return new()
        {
            End = period.End.ToString(TimeOfDay),
            Start = period.Start.ToString(TimeOfDay)
        };
    }

    private static bool EndsIn(this Period period, Period other)
    {
        return other.Start <= period.End && period.End <= other.End;
    }

    private static bool StartsIn(this Period period, Period other)
    {
        return other.Start <= period.Start && period.Start <= other.End;
    }
}
