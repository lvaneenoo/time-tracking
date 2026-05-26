using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;

internal static class PeriodQueries
{
    private const string Format = "HH:mm";

    private const string PeriodEnd = "@period_end";
    private const string PeriodStart = "@period_start";

    public static bool Overlaps(this Period period, Period other) => period.StartsIn(other) || period.EndsIn(other);

    public static ReadOnlyCollection<SqliteParameter> ResolveParameters(this Period period) =>
        [
            new(PeriodStart, SqliteType.Text)
            {
                Value = period.Start.ToString(Format)
            },
            new(PeriodEnd, SqliteType.Text)
            {
                Value = period.End.ToString(Format)
            }
        ];

    private static bool EndsIn(this Period period, Period other) =>
        other.Start <= period.End && period.End <= other.End;

    private static bool StartsIn(this Period period, Period other) =>
        other.Start <= period.Start && period.Start <= other.End;
}
