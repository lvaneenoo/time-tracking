namespace TimeSheetTests;

internal class CtorTestArgs : TheoryData<TrackedDate, IList<TimeSheetEntry>, TimeSheetStatus>
{
    public CtorTestArgs()
    {
        Add(new TrackedDate(DateOnly.MinValue), [], TimeSheetStatus.Created);
    }
}
