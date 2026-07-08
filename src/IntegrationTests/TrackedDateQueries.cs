internal static class TrackedDateQueries
{
    public static TimeSheetSnapshot CreateTimeSheet(this TrackedDate date)
    {
        return new(new TimeSheet(date, [], TimeSheetStatus.Created))
        {
            ModifiedOn = DateTimeOffset.MinValue
        };
    }
}
