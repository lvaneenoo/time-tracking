public class TimeSheetSnapshot(TrackedDate date, IList<TimeSheetEntry> entries, TimesheetStatus status)
    : TimeSheet(date, entries, status)
{
    public required DateTimeOffset ModifiedOn { get; init; }
}
