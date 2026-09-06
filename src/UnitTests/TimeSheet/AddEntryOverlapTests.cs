namespace TimeSheetTests;

public class AddEntryOverlapTests
{
    [Fact]
    public void Test()
    {
        var sut = new TimeSheet(
            new TrackedDate(DateOnly.MinValue),
            [new TimeSheetEntry(new Period(TimeOnly.MinValue, TimeOnly.MinValue), new Comment(""))],
            TimeSheetStatus.Created);

        var (sheet, entry) = sut.AddEntry(sut.Entries[0].Period, sut.Entries[0].Comment);

        Assert.Same(sut, sheet);
        Assert.Null(entry);
    }
}
