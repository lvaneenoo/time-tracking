using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;

internal static class PeriodQueries
{
    private const string Format = "HH:mm";

    private const string PeriodEnd = "@period_end";
    private const string PeriodStart = "@period_start";

    public static bool Overlaps(this Period period, Period other) => period.StartsIn(other) || period.EndsIn(other);

    public static ReadOnlyCollection<SqliteParameter> ResolveParameters(this Period period)
    {
        return
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
    }

    public static PeriodResource ToResource(this Period period)
    {
        return new()
        {
            End = period.End.ToString(Format),
            Start = period.Start.ToString(Format)
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
