namespace TestFloor;

internal static class Some
{
    public static readonly Comment Comment = new("");
    public static readonly Period Period = new(TimeOnly.MinValue, TimeOnly.MinValue);
    public static readonly TimeSheetEntry TimeSheetEntry = new(Period, Comment);
    public static readonly TimeSheetStatus TimeSheetStatus = TimeSheetStatus.Created;
    public static readonly TrackedDate TrackedDate = new(DateOnly.MinValue);
}
