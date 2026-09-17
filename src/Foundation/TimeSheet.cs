public class TimeSheet
{
    internal TimeSheet(TrackedDate date, IList<TimeSheetEntry> entries, TimeSheetStatus status)
    {
        Date = date;
        Entries = new TimeSheetEntryCollection(entries);
        Status = status;
    }

    public TrackedDate Date { get; }
    public TimeSheetEntryCollection Entries { get; }
    public TimeSheetStatus Status { get; }
}
