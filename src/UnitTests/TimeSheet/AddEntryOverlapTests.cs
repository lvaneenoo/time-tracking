namespace TimeSheetTests;

public class AddEntryOverlapTests
{
    [Fact]
    public void Test()
    {
        var period = new Period(TimeOnly.MinValue, TimeOnly.MinValue);
        var sut = new TimeSheet(new (DateOnly.MinValue), [new (period, "")], TimeSheetStatus.Created);

        var (sheet, entry) = sut.AddEntry(period, "");

        Assert.Same(sut, sheet);
        Assert.Null(entry);
    }
}
