namespace TestFloor;

internal static class Specimen
{
    public static readonly Comment Comment = Comment.Empty;
    public static readonly Period Period = new(TimeOnly.MinValue, TimeOnly.MinValue);
    public static readonly TimesheetEntry TimesheetEntry = new(Period, Comment);
    public static readonly TimesheetStatus TimesheetStatus = TimesheetStatus.Created;
    public static readonly TrackedDate TrackedDate = new(DateOnly.MinValue);
}
