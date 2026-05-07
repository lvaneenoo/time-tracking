namespace TimeSheetTests;

public class AddEntryOverlapTests
{
    [Theory]
    [ClassData(typeof(AddEntryOverlapTestArgs))]
    public void TestImmutability(TimeSheet sut, Period period)
    {
        var (sheet, _) = sut.AddEntry(period);

        Assert.Same(sut, sheet);
    }

    [Theory]
    [ClassData(typeof(AddEntryOverlapTestArgs))]
    public void TestTransferability(TimeSheet sut, Period period)
    {
        var (_, entry) = sut.AddEntry(period);

        Assert.Null(entry);
    }
}
