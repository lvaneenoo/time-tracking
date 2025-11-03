using System.Diagnostics.CodeAnalysis;

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

    public bool TryAddEntry(Period period, [NotNullWhen(true)] out TimeSheet? result)
    {
        if (Entries.Any(entry => entry.Period.Overlaps(period)))
        {
            result = null;
            return false;
        }

        result = new TimeSheet(Date, [.. Entries, new TimeSheetEntry(period)], Status);
        return true;
    }
}
