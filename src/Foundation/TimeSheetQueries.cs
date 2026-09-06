public static class TimeSheetQueries
{
    public static (TimeSheet, TimeSheetEntry?) AddEntry(this TimeSheet sheet, Period period, Comment comment)
    {
        if (sheet.Entries.Any(entry => period.Overlaps(entry.Period)))
        {
            return (sheet, null);
        }

        var candidate = new TimeSheetEntry(period, comment);

        return (sheet.Create([.. sheet.Entries, candidate]), candidate);
    }

    internal static TimeSheet Create(this TimeSheet sheet, IList<TimeSheetEntry> entries)
    {
        return new TimeSheet(sheet.Date, entries, sheet.Status);
    }
}
