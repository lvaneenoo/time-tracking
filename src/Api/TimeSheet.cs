internal class TimeSheet
{
    private TimeSheet(TrackedDate date, IList<TimeSheetEntry> entries)
    {
        Date = date;
        Entries = [..entries];
    }

    public TrackedDate Date { get; }
    public IReadOnlyList<TimeSheetEntry> Entries { get; }

    public static TimeSheet Create(TrackedDate date, IList<TimeSheetEntry> entries) => new(date, entries);

    public TimeSheet AddEntry(Period period)
    {
        if (Entries.Any(entry => entry.Period.Overlaps(period)))
        {
            throw new InvalidOperationException();
        }

        return new TimeSheet(Date, [.. Entries, new TimeSheetEntry(period)]);
    }

    public override int GetHashCode() => Date.GetHashCode();
    public override string ToString() => Date.ToString();
}
