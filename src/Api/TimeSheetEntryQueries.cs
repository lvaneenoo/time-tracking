internal static class TimeSheetEntryQueries
{
    public static TimeSheetEntryResource ToResource(this TimeSheetEntry entry)
    {
        var snapshot = (TimeSheetEntrySnapshot)entry;

        return new TimeSheetEntryResource
        {
            Id = snapshot.Id,
            Period = new PeriodResource
            {
                End = entry.Period.End.ToString("HH:mm"),
                Start = entry.Period.Start.ToString("HH:mm")
            }
        };
    }
}
