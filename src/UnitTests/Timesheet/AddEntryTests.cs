using TestFloor;

namespace TimesheetTests;

public class AddEntryTests
{
    [Fact]
    public void Test()
    {
        var sut = new Timesheet(Specimen.TrackedDate, [], Specimen.TimesheetStatus);

        var (sheet, entry) = sut.AddEntry(Specimen.Period, Specimen.Comment);

        Assert.NotSame(sut, sheet);
        Assert.Equal(sut.Date, sheet.Date);
        Assert.Equal(sut.Status, sheet.Status);

        Assert.NotNull(entry);
        Assert.Same(Specimen.Period, entry.Period);
        Assert.Same(Specimen.Comment, entry.Comment);

        Assert.Single(sheet.Entries);
        Assert.Same(entry, sheet.Entries[0]);
    }
}
