public class TimesheetSnapshot(TrackedDate date, IList<TimesheetEntry> entries, TimesheetStatus status)
    : Timesheet(date, entries, status)
{
    public required DateTimeOffset ModifiedOn { get; init; }
}
