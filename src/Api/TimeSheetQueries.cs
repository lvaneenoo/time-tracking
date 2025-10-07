internal static class TimeSheetQueries
{
    public static TimeSheetResource ToResource(this TimeSheet timeSheet) =>
        new()
        {
            Date = timeSheet.Date.ToString(),
            Entries = [.. timeSheet.Entries.Select(entry => entry.ToResource())]
        };
}
