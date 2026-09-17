using TestFloor;

namespace TimeSheetTests;

public class CtorTests
{
    [Fact]
    public void Test()
    {
        var sheet = new TimeSheet(Some.TrackedDate, [], Some.TimeSheetStatus);

        Assert.Equal(Some.TrackedDate, sheet.Date);
        Assert.Empty(sheet.Entries);
        Assert.Equal(Some.TimeSheetStatus, sheet.Status);
    }
}
