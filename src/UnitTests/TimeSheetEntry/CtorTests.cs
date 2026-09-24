using TestFloor;

namespace TimeSheetEntryTests;

public class CtorTests
{
    [Fact]
    public void Test()
    {
        var period = Some.Period;
        var comment = Some.Comment;

        var entry = new TimeSheetEntry(period, comment);

        Assert.Equal(period, entry.Period);
        Assert.Equal(comment, entry.Comment);
    }
}
