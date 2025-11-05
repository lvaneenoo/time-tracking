internal class TimeSheet
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

    public override int GetHashCode() => Date.GetHashCode();
    public override string ToString() => Date.ToString();

    public (TimeSheet Timesheet, TimeSheetEntry? entry) AddEntry(Period period)
    {
        if (Entries.Any(entry => entry.Period.Overlaps(period)))
        {
            return (this, null);
        }

        var candidate = new TimeSheetEntry(period);

        return (new TimeSheet(Date, [.. Entries, candidate], Status), candidate);
    }
}
