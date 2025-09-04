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
}
