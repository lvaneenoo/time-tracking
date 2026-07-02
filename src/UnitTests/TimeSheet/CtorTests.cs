namespace TimeSheetTests;

public class CtorTests
{
    [Fact]
    public void Test()
    {
        var date = new TrackedDate(DateOnly.MinValue);

        var sheet = new TimeSheet(date, [], TimeSheetStatus.Created);

        Assert.Equal(date, sheet.Date);
        Assert.Empty(sheet.Entries);
        Assert.Equal(TimeSheetStatus.Created, sheet.Status);
    }
}
