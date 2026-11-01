using TestFloor;

namespace TimeSheetTests;

public class CtorTests
{
    [Fact]
    public void Test()
    {
        var date = Specimen.TrackedDate;
        var entries = new List<TimesheetEntry>();
        var status = Specimen.TimesheetStatus;

        var sheet = new TimeSheet(date, entries, status);

        Assert.Equal(date, sheet.Date);
        Assert.Equal(entries.Count, sheet.Entries.Count);
        Assert.Equal(status, sheet.Status);
    }
}
