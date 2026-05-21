internal class DeleteTimeSheetEntries
{
    public static string ByDateAndPeriod => "DELETE FROM time_sheet_entries WHERE time_sheet_entries.time_sheet_date = @time_sheet_date AND time_sheet_entries.period_start = @period_start AND time_sheet_entries.period_end = @period_end";
}
