internal class RetrieveTimeSheets
{
    public static string ByDate => "SELECT time_sheets.time_sheet_date, time_sheets.time_sheet_status, time_sheet_entries.rowid, time_sheet_entries.period_start, time_sheet_entries.period_end FROM time_sheets LEFT JOIN time_sheet_entries ON time_sheets.time_sheet_date = time_sheet_entries.time_sheet_date WHERE time_sheets.time_sheet_date = @time_sheet_date";
    public static string ByEntryId => "SELECT time_sheets.time_sheet_date, time_sheets.time_sheet_status, time_sheet_entries.rowid, time_sheet_entries.period_start, time_sheet_entries.period_end FROM time_sheets LEFT JOIN time_sheet_entries ON time_sheets.time_sheet_date = time_sheet_entries.time_sheet_date WHERE time_sheets.time_sheet_date IN (SELECT time_sheet_entries.time_sheet_date FROM time_sheet_entries WHERE time_sheet_entries.rowid = @rowid)";
}
