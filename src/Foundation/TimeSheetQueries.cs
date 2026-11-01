public static class TimeSheetQueries
{
    public static (TimeSheet, TimesheetEntry?) AddEntry(this TimeSheet sheet, Period period, Comment comment)
    {
        if (sheet.Entries.Any(entry => period.Overlaps(entry.Period)))
        {
            return (sheet, null);
        }

        var candidate = new TimesheetEntry(period, comment);

        var entries = new List<TimesheetEntry>(sheet.Entries)
        {
            candidate
        };

        entries.Sort(ByPeriod);

        return (sheet.Create(entries), candidate);
    }

    internal static TimeSheet Create(this TimeSheet sheet, IList<TimesheetEntry> entries)
    {
        return new TimeSheet(sheet.Date, entries, sheet.Status);
    }

    private static int ByPeriod(TimesheetEntry x, TimesheetEntry y) => x.Period.CompareTo(y.Period);
}
