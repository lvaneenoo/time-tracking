namespace TimeSheetTests;

public class AddEntryTests
{
    [Theory]
    [ClassData(typeof(AddEntryTestArgs))]
    public void TestImmutability(TimeSheet sut, Period period)
    {
        var (sheet, _) = sut.AddEntry(period);

        Assert.NotSame(sut, sheet);
    }

    [Theory]
    [ClassData(typeof(AddEntryTestArgs))]
    public void TestTransferability(TimeSheet sut, Period period)
    {
        var (_, entry) = sut.AddEntry(period);

        Assert.True(entry is not null && period == entry.Period);
    }
}
