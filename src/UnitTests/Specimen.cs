namespace TestFloor;

internal static class Specimen
{
    public static readonly Comment Comment = Comment.Empty;
    public static readonly Period Period = new(TimeOnly.MinValue, TimeOnly.MinValue);
    public static readonly TimeSheetEntry TimeSheetEntry = new(Period, Comment);
    public static readonly TimeSheetStatus TimeSheetStatus = TimeSheetStatus.Created;
    public static readonly TrackedDate TrackedDate = new(DateOnly.MinValue);
}
