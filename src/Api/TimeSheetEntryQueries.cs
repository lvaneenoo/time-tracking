internal static class TimeSheetEntryQueries
{
    public static TimeSheetEntryResource ToResource(this TimeSheetEntry entry)
    {
        return new TimeSheetEntryResource
        {
            Comment = entry.Comment,
            Period = entry.Period.ToResource()
        };
    }
}
