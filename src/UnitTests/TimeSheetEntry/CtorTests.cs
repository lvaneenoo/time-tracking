namespace TimeSheetEntryTests;

public class CtorTests
{
    [Fact]
    public void Test()
    {
        var period = new Period(TimeOnly.MinValue, TimeOnly.MinValue);
        var comment = new Comment("");

        var entry = new TimeSheetEntry(period, comment);

        Assert.Equal(period, entry.Period);
        Assert.Equal(comment, entry.Comment);
    }
}
