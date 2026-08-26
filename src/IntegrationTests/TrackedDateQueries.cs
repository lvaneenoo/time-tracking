internal static class TrackedDateQueries
{
    public static TimeSheetSnapshot CreateTimeSheet(this TrackedDate date)
    {
        return new TimeSheetSnapshot(date, [], TimeSheetStatus.Created)
        {
            ModifiedOn = DateTimeOffset.MinValue
        };
    }
}
