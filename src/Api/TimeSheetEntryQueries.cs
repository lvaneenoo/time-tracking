internal static class TimeSheetEntryQueries
{
    public static TimeSheetEntryResource ToResource(this TimeSheetEntry entry)
    {
        return new TimeSheetEntryResource
        {
            Period = entry.Period.ToResource()
        };
    }
}
