internal static class TrackedDateQueries
{
    public static TimeSheet Resolve(this TrackedDate date)
    {
        return new TimeSheetSnapshot(new(date, [], TimeSheetStatus.Created))
        {
            ModifiedOn = DateTimeOffset.MinValue
        };
    }
}
