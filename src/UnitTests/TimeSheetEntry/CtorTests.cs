namespace TimeSheetEntryTests;

public class CtorTests
{
    [Theory]
    [ClassData(typeof(CtorTestArgs))]
    public void TestCtor(Period period)
    {
        var entry = new TimeSheetEntry(period);

        Assert.Equal(period, entry.Period);
    }
}
