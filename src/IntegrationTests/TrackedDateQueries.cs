using TestFloor;

internal static class TrackedDateQueries
{
    public static TimeSheetSnapshot NineToFive(this TrackedDate date) => Create(date, Entries.NineToFive);
    public static TimeSheetSnapshot NoEntries(this TrackedDate date) => Create(date, []);

    private static TimeSheetSnapshot Create(TrackedDate date, IList<TimeSheetEntry> entries)
    {
        return new TimeSheetSnapshot(date, entries, TimeSheetStatus.Created)
        {
            ModifiedOn = new DateTimeOffset(new DateTime(date, TimeOnly.MinValue))
        };
    }
}
