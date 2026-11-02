using TestFloor;

namespace TimesheetTests;

public class CreationTests
{
    [Fact]
    public void Test()
    {
        var sut = new Timesheet(Specimen.TrackedDate, [], Specimen.TimesheetStatus);

        var sheet = sut.Create([Specimen.TimesheetEntry]);

        Assert.NotSame(sut, sheet);
        Assert.Equal(sut.Date, sheet.Date);
        Assert.Equal(sut.Status, sheet.Status);

        Assert.Single(sheet.Entries);
        Assert.Same(Specimen.TimesheetEntry, sheet.Entries[0]);
    }
}
