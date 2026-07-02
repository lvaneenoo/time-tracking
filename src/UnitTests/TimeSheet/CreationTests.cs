namespace TimeSheetTests;

public class CreationTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(new (DateOnly.MinValue), [], TimeSheetStatus.Created);
        var entry = new TimeSheetEntry(new (TimeOnly.MinValue, TimeOnly.MinValue), "");

        var sheet = sut.Create([entry]);

        Assert.NotSame(sut, sheet);
        Assert.Equal(sut.Date, sheet.Date);
        Assert.Equal(sut.Status, sheet.Status);

        Assert.Single(sheet.Entries);
        Assert.Same(entry, sheet.Entries[0]);
    }
}
