internal static class TimeSheetQueries
{
    public static TimeSheet AddEntry(this TimeSheet timeSheet, Period period)
    {
        return new TimeSheet(timeSheet.Date, [.. timeSheet.Entries, new(period)]);
    }

    public static TimeSheetResource ToResource(this TimeSheet timeSheet) =>
        new()
        {
            Date = timeSheet.Date.ToString(),
            Entries = [.. timeSheet.Entries.Select(entry => entry.ToResource())]
        };
}
