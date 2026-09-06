namespace TimeSheetTests;

public class AddEntryTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(new TrackedDate(DateOnly.MinValue), [], TimeSheetStatus.Created);

        var period = new Period(TimeOnly.MinValue, TimeOnly.MinValue);
        var comment = new Comment("");

        var (sheet, entry) = sut.AddEntry(period, comment);

        Assert.NotSame(sut, sheet);
        Assert.Equal(sut.Date, sheet.Date);
        Assert.Equal(sut.Status, sheet.Status);

        Assert.NotNull(entry);
        Assert.Same(period, entry.Period);
        Assert.Same(comment, entry.Comment);

        Assert.Single(sheet.Entries);
        Assert.Same(entry, sheet.Entries[0]);
    }
}
