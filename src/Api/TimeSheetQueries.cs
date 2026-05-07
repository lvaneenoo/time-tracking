public static class TimeSheetQueries
{
    public static (TimeSheet, TimeSheetEntry?) AddEntry(this TimeSheet sheet, Period period)
    {
        if (sheet.Entries.Any(entry => entry.Period.Overlaps(period)))
        {
            return (sheet, null);
        }

        var candidate = new TimeSheetEntry(period);

        return (sheet.Create([.. sheet.Entries, candidate]), candidate);
    }
}
