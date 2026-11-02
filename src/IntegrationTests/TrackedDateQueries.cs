namespace TestFloor;

internal static class TrackedDateQueries
{
    public static TimesheetSnapshot NineToFive(this TrackedDate date) => Create(date, Entries.NineToFive);
    public static TimesheetSnapshot NoEntries(this TrackedDate date) => Create(date, []);

    private static TimesheetSnapshot Create(TrackedDate date, IList<TimesheetEntry> entries)
    {
        return new TimesheetSnapshot(date, entries, TimesheetStatus.Created)
        {
            ModifiedOn = new DateTimeOffset(new DateTime(date, TimeOnly.MinValue))
        };
    }

    internal static class Entries
    {
        public static readonly List<TimesheetEntry> NineToFive =
        [
            new TimesheetEntry(new Period(new TimeOnly(9, 0), new TimeOnly(16, 59)), Comment.Empty)
        ];
    }
}
