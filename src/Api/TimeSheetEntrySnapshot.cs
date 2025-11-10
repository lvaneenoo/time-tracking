internal class TimeSheetEntrySnapshot : TimeSheetEntry
{
    internal TimeSheetEntrySnapshot(TimeSheetEntry entry) : base(entry.Period) { }

    public long Id { get; init; }
}
