internal class TimeSheetEntry
{
    internal TimeSheetEntry(Period period)
    {
        Period = period;
    }

    public Period Period { get; }

    public override int GetHashCode() => Period.GetHashCode();
    public override string ToString() => Period.ToString();
}
