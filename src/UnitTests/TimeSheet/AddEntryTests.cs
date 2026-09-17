using TestFloor;

namespace TimeSheetTests;

public class AddEntryTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(Some.TrackedDate, [], Some.TimeSheetStatus);

        var (sheet, entry) = sut.AddEntry(Some.Period, Some.Comment);

        Assert.NotSame(sut, sheet);
        Assert.Equal(sut.Date, sheet.Date);
        Assert.Equal(sut.Status, sheet.Status);

        Assert.NotNull(entry);
        Assert.Same(Some.Period, entry.Period);
        Assert.Same(Some.Comment, entry.Comment);

        Assert.Single(sheet.Entries);
        Assert.Same(entry, sheet.Entries[0]);
    }
}
