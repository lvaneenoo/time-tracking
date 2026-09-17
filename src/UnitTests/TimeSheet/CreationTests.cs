using TestFloor;

namespace TimeSheetTests;

public class CreationTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(Some.TrackedDate, [], Some.TimeSheetStatus);

        var sheet = sut.Create([Some.TimeSheetEntry]);

        Assert.NotSame(sut, sheet);
        Assert.Equal(sut.Date, sheet.Date);
        Assert.Equal(sut.Status, sheet.Status);

        Assert.Single(sheet.Entries);
        Assert.Same(Some.TimeSheetEntry, sheet.Entries[0]);
    }
}
