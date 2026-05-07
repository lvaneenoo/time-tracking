namespace TimeSheetTests;

internal class AddEntryTestArgs : TheoryData<TimeSheet, Period>
{
    public AddEntryTestArgs()
    {
        Add(
            new TimeSheet(new TrackedDate(DateOnly.MinValue), [], TimeSheetStatus.Created),
            new Period(TimeOnly.MinValue, TimeOnly.MinValue));
    }
}
