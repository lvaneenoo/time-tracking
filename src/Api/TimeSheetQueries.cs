internal static class TimeSheetQueries
{
    public static TimeSheet AddEntry(this TimeSheet timeSheet, Period period)
    {
        return new TimeSheet(timeSheet.Date, [..timeSheet.Entries, new(period)]);
    }
}
