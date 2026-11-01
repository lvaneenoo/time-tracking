using TestFloor;

namespace TimeSheetEntryTests;

public class CtorTests
{
    [Fact]
    public void Test()
    {
        var period = Specimen.Period;
        var comment = Specimen.Comment;

        var entry = new TimesheetEntry(period, comment);

        Assert.Equal(period, entry.Period);
        Assert.Equal(comment, entry.Comment);
    }
}
