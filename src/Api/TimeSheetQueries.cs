internal static class TimeSheetQueries
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

    public static TimeSheetResource ToResource(this TimeSheet sheet)
    {
        return new TimeSheetResource
        {
            Date = sheet.Date.ToString(),
            Entries = [.. sheet.Entries.Select(entry => entry.ToResource())],
            Status = (int)sheet.Status
        };
    }
}
