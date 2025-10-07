internal static class PeriodQueries
{
    public static bool Overlaps(this Period period, Period other) => period.StartsIn(other) || period.EndsIn(other);

    private static bool EndsIn(this Period period, Period other) =>
        other.Start <= period.End && period.End <= other.End;

    private static bool StartsIn(this Period period, Period other) =>
        other.Start <= period.Start && period.Start <= other.End;
}
