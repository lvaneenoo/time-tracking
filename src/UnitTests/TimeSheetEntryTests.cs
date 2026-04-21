namespace UnitTests;

public class TimeSheetEntryTests
{
    public static TheoryData<Period> GetData()
    {
        return new()
        {
            { new Period(TimeOnly.MinValue, TimeOnly.MinValue) }
        };
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void TestCtor(Period period)
    {
        var entry = new TimeSheetEntry(period);

        Assert.Equal(period, entry.Period);
    }
}
