internal class TimeSheetEntrySnapshot : TimeSheetEntry
{
    internal TimeSheetEntrySnapshot(TimeSheetEntry entry) : base(entry.Period) { }

    public required TimeSheetEntryId Id { get; init; }
}
