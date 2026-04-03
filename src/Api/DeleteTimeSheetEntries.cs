internal class DeleteTimeSheetEntries
{
    public static string ById => "DELETE FROM time_sheet_entries WHERE time_sheet_entries.rowid = @rowid";
}
