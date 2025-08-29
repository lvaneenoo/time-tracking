internal class TimeSheet
{
    internal TimeSheet(TrackedDate date)
    {
        Date = date;
    }

    public TrackedDate Date { get; }

    public override int GetHashCode() => Date.GetHashCode();
    public override string ToString() => Date.ToString();
}
