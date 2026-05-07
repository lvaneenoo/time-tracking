public class TimeSheet
{
    internal TimeSheet(TrackedDate date, IList<TimeSheetEntry> entries, TimeSheetStatus status)
    {
        Date = date;
        Entries = [.. entries];
        Status = status;
    }

    public TrackedDate Date { get; }
    public IReadOnlyList<TimeSheetEntry> Entries { get; }
    public TimeSheetStatus Status { get; }

    internal TimeSheet Create(IList<TimeSheetEntry> entries) => new(Date, entries, Status);
}
