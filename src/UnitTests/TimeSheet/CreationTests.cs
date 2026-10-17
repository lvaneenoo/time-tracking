using TestFloor;

namespace TimeSheetTests;

public class CreationTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(Specimen.TrackedDate, [], Specimen.TimeSheetStatus);

        var sheet = sut.Create([Specimen.TimeSheetEntry]);

        Assert.NotSame(sut, sheet);
        Assert.Equal(sut.Date, sheet.Date);
        Assert.Equal(sut.Status, sheet.Status);

        Assert.Single(sheet.Entries);
        Assert.Same(Specimen.TimeSheetEntry, sheet.Entries[0]);
    }
}
