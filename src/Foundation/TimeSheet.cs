public class TimeSheet
{
    internal TimeSheet(TrackedDate date, IList<TimesheetEntry> entries, TimesheetStatus status)
    {
        Date = date;
        Entries = new TimesheetEntryCollection(entries);
        Status = status;
    }

    public TrackedDate Date { get; }
    public TimesheetEntryCollection Entries { get; }
    public TimesheetStatus Status { get; }
}
