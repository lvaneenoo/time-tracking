internal class DeleteTimeSheetEntries
{
    public static readonly string ByTimeSheetEntryId = "DELETE FROM time_sheet_entries WHERE time_sheet_entries.rowid = @rowid";
}
