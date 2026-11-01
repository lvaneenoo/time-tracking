public class TimeSheetSnapshot(TrackedDate date, IList<TimesheetEntry> entries, TimesheetStatus status)
    : TimeSheet(date, entries, status)
{
    public required DateTimeOffset ModifiedOn { get; init; }
}
