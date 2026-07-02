namespace TimeSheetEntryTests;

public class CtorTests
{
    [Fact]
    public void Test()
    {
        var period = new Period(TimeOnly.MinValue, TimeOnly.MinValue);

        var entry = new TimeSheetEntry(period, "");

        Assert.Equal(period, entry.Period);
        Assert.Equal("", entry.Comment);
    }
}
