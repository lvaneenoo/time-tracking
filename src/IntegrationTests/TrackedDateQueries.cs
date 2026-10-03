using TestFloor;

internal static class TrackedDateQueries
{
    public static TimeSheetSnapshot CreateTimeSheet(this TrackedDate date)
    {
        return new TimeSheetSnapshot(date, [], Some.TimeSheetStatus)
        {
            ModifiedOn = new DateTimeOffset(new DateTime(date, TimeOnly.MinValue))
        };
    }

    public static TimeSheetSnapshot NineToFive(this TrackedDate date)
    {
        return new TimeSheetSnapshot(date, Entries.NineToFive, Some.TimeSheetStatus)
        {
            ModifiedOn = new DateTimeOffset(new DateTime(date, TimeOnly.MinValue))
        };
    }
}
