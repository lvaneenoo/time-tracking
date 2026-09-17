using TestFloor;

namespace TimeSheetTests;

public class AddEntryOverlapTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(Some.TrackedDate, [Some.TimeSheetEntry], Some.TimeSheetStatus);

        var (sheet, entry) = sut.AddEntry(Some.TimeSheetEntry.Period, Some.TimeSheetEntry.Comment);

        Assert.Same(sut, sheet);
        Assert.Null(entry);
    }
}
