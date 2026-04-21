namespace UnitTests;

public class TimeSheetTests
{
    public static TheoryData<TrackedDate, IList<TimeSheetEntry>, TimeSheetStatus> GetData()
    {
        return new()
        {
            { new TrackedDate(DateOnly.MinValue), [], TimeSheetStatus.Created }
        };
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void TestCtor(TrackedDate date, IList<TimeSheetEntry> entries, TimeSheetStatus status)
    {
        var sheet = new TimeSheet(date, entries, status);

        Assert.Equal(date, sheet.Date);
        Assert.Equal(entries.Count, sheet.Entries.Count);
        Assert.Equal(status, sheet.Status);
    }
}
