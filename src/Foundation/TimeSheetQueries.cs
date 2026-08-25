public static class TimeSheetQueries
{
    public static (TimeSheet, TimeSheetEntry?) AddEntry(this TimeSheet sheet, Period period, string comment)
    {
        if (sheet.Entries.Any(entry => entry.Period.Overlaps(period)))
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
