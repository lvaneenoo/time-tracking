using TestFloor;

namespace TimeSheetTests;

public class AddEntryOverlapTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(Specimen.TrackedDate, [Specimen.TimeSheetEntry], Specimen.TimeSheetStatus);

        var (sheet, entry) = sut.AddEntry(Specimen.TimeSheetEntry.Period, Specimen.TimeSheetEntry.Comment);

        Assert.Same(sut, sheet);
        Assert.Null(entry);
    }
}
