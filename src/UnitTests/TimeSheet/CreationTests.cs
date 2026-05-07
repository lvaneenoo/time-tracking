namespace TimeSheetTests;

public class CreationTests
{
    [Theory]
    [ClassData(typeof(CreationTestArgs))]
    public void TestImmutability(TimeSheet sut, TimeSheetEntry entry)
    {
        Assert.NotSame(sut, sut.Create([entry]));
    }

    [Theory]
    [ClassData(typeof(CreationTestArgs))]
    public void TestTransferability(TimeSheet sut, TimeSheetEntry entry)
    {
        var sheet = sut.Create([entry]);

        Assert.Equal(sut.Date, sheet.Date);
        Assert.True(sheet.Entries.Count == 1 && entry == sheet.Entries[0]);
        Assert.Equal(sut.Status, sheet.Status);
    }
}
