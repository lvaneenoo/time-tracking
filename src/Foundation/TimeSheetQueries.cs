public static class TimeSheetQueries
{
    public static (TimeSheet, TimeSheetEntry?) AddEntry(this TimeSheet sheet, Period period, Comment comment)
    {
        if (sheet.Entries.Any(entry => period.Overlaps(entry.Period)))
        {
            return (sheet, null);
        }

        var candidate = new TimeSheetEntry(period, comment);

        var entries = new List<TimeSheetEntry>(sheet.Entries)
        {
            candidate
        };

        entries.Sort(ByPeriod);

        return (sheet.Create(entries), candidate);
    }

    internal static TimeSheet Create(this TimeSheet sheet, IList<TimeSheetEntry> entries)
    {
        return new TimeSheet(sheet.Date, entries, sheet.Status);
    }

    private static int ByPeriod(TimeSheetEntry x, TimeSheetEntry y) => x.Period.CompareTo(y.Period);
}
