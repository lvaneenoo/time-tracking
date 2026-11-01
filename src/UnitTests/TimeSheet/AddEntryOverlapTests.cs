using TestFloor;

namespace TimeSheetTests;

public class AddEntryOverlapTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(Specimen.TrackedDate, [Specimen.TimesheetEntry], Specimen.TimesheetStatus);

        var (sheet, entry) = sut.AddEntry(Specimen.TimesheetEntry.Period, Specimen.TimesheetEntry.Comment);

        Assert.Same(sut, sheet);
        Assert.Null(entry);
    }
}
