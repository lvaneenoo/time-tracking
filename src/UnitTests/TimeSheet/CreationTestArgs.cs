namespace TimeSheetTests;

internal class CreationTestArgs : TheoryData<TimeSheet, TimeSheetEntry>
{
    public CreationTestArgs()
    {
        Add(
            new TimeSheet(new TrackedDate(DateOnly.MinValue), [], TimeSheetStatus.Created),
            new TimeSheetEntry(new Period(TimeOnly.MinValue, TimeOnly.MinValue)));
    }
}
