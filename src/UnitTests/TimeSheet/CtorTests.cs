using TestFloor;

namespace TimeSheetTests;

public class CtorTests
{
    [Fact]
    public void Test()
    {
        var date = Some.TrackedDate;
        var entries = new List<TimeSheetEntry>();
        var status = Some.TimeSheetStatus;

        var sheet = new TimeSheet(date, entries, status);

        Assert.Equal(date, sheet.Date);
        Assert.Equal(entries.Count, sheet.Entries.Count);
        Assert.Equal(status, sheet.Status);
    }
}
