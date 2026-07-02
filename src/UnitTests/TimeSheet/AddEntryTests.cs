namespace TimeSheetTests;

public class AddEntryTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(new (DateOnly.MinValue), [], TimeSheetStatus.Created);
        var period = new Period(TimeOnly.MinValue, TimeOnly.MinValue);

        var (sheet, entry) = sut.AddEntry(period, "");

        Assert.NotSame(sut, sheet);
        Assert.Equal(sut.Date, sheet.Date);
        Assert.Equal(sut.Status, sheet.Status);

        Assert.NotNull(entry);
        Assert.Same(period, entry.Period);
        Assert.Same("", entry.Comment);

        Assert.Single(sheet.Entries);
        Assert.Same(entry, sheet.Entries[0]);
    }
}
