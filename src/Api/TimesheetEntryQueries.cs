internal static class TimeSheetEntryQueries
{
    public static TimesheetEntryResource ToResource(this TimesheetEntry entry)
    {
        return new TimesheetEntryResource
        {
            Comment = entry.Comment,
            Period = entry.Period.ToResource()
        };
    }
}
