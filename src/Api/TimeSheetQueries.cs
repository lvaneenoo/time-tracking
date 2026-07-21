internal static class TimeSheetQueries
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

    public static string CreateResourceId(this TimeSheet sheet)
    {
        var snapshot = (TimeSheetSnapshot)sheet;

        return $"{Math.Abs(snapshot.ModifiedOn.GetHashCode())}";
    }

    public static TimeSheetResource ToResource(this TimeSheet sheet)
    {
        return new TimeSheetResource
        {
            Date = sheet.Date.ToString("yyyy-MM-dd", null),
            Entries = [.. sheet.Entries.Select(entry => entry.ToResource())],
            Status = (int)sheet.Status
        };
    }
}
