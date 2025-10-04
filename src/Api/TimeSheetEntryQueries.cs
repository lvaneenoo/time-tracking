internal static class TimeSheetEntryQueries
{
    public static TimeSheetEntryResource ToResource(this TimeSheetEntry entry) =>
        new()
        {
            End = entry.Period.End.ToString("HH:mm"),
            Start = entry.Period.Start.ToString("HH:mm")
        };
}
