public class TimeSheetEntry
{
    internal TimeSheetEntry(Period period, string comment)
    {
        Period = period;
        Comment = comment;
    }

    public string Comment { get; }
    public Period Period { get; }
}
