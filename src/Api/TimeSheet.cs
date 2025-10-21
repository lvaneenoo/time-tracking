internal class TimeSheet
{
    internal TimeSheet(TrackedDate date, IList<TimeSheetEntry> entries)
    {
        Date = date;
        Entries = [.. entries];
    }

    public TrackedDate Date { get; }
    public IReadOnlyList<TimeSheetEntry> Entries { get; }

    public override int GetHashCode() => Date.GetHashCode();
    public override string ToString() => Date.ToString();

    public bool TryAddEntry(Period period, out TimeSheet? result)
    {
        if (Entries.Any(entry => entry.Period.Overlaps(period)))
        {
            result = null;
            return false;
        }

        result = new TimeSheet(Date, [.. Entries, new TimeSheetEntry(period)]);
        return true;
    }
}
