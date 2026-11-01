public class TimesheetEntry
{
    internal TimesheetEntry(Period period, Comment comment)
    {
        Period = period;
        Comment = comment;
    }

    public Comment Comment { get; }
    public Period Period { get; }
}
