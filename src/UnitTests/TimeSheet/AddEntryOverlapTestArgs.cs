namespace TimeSheetTests;

internal class AddEntryOverlapTestArgs : TheoryData<TimeSheet, Period>
{
    public AddEntryOverlapTestArgs()
    {
        var sheet = new TimeSheet(
            new TrackedDate(DateOnly.MinValue),
            [new TimeSheetEntry(new Period(TimeOnly.MinValue, TimeOnly.MinValue))],
            TimeSheetStatus.Created);

        Add(sheet, sheet.Entries[0].Period);
    }
}
