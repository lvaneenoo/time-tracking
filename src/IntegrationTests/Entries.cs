namespace TestFloor;

internal static class Entries
{
    private static readonly TimeOnly Five = new(16, 59);
    private static readonly TimeOnly Nine = new(9, 0);

    public static List<TimeSheetEntry> NineToFive = [new TimeSheetEntry(new Period(Nine, Five), Some.Comment)];
}
