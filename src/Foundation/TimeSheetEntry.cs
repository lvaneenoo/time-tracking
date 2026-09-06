public class TimeSheetEntry
{
    internal TimeSheetEntry(Period period, Comment comment)
    {
        Period = period;
        Comment = comment;
    }

    public Comment Comment { get; }
    public Period Period { get; }
}
