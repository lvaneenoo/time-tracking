namespace TimeSheetTests;

public class CtorTests
{
    [Theory]
    [ClassData(typeof(CtorTestArgs))]
    public void TestCtor(TrackedDate date, IList<TimeSheetEntry> entries, TimeSheetStatus status)
    {
        var sheet = new TimeSheet(date, entries, status);

        Assert.Equal(date, sheet.Date);
        Assert.Equal(entries.Count, sheet.Entries.Count);
        Assert.Equal(status, sheet.Status);
    }
}
