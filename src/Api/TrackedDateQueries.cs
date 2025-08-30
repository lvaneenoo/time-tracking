internal static class TrackedDateQueries
{
    public static bool IsWeekend(this TrackedDate date) =>
        date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
}
