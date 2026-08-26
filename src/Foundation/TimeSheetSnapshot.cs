public class TimeSheetSnapshot(TrackedDate date, IList<TimeSheetEntry> entries, TimeSheetStatus status)
    : TimeSheet(date, entries, status)
{
    public required DateTimeOffset ModifiedOn { get; init; }
}
