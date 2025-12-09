using Microsoft.Data.Sqlite;

internal class RetrieveTimeSheets
{
    internal class CommandTextValues
    {
        public static readonly string ByTimeSheetDate = "SELECT time_sheets.time_sheet_date, time_sheets.time_sheet_status, time_sheet_entries.rowid, time_sheet_entries.period_start, time_sheet_entries.period_end FROM time_sheets LEFT JOIN time_sheet_entries ON time_sheets.time_sheet_date = time_sheet_entries.time_sheet_date WHERE time_sheets.time_sheet_date = @time_sheet_date";
        public static readonly string ByTimeSheetEntryId = "SELECT time_sheets.time_sheet_date, time_sheets.time_sheet_status, time_sheet_entries.rowid, time_sheet_entries.period_start, time_sheet_entries.period_end FROM time_sheets LEFT JOIN time_sheet_entries ON time_sheets.time_sheet_date = time_sheet_entries.time_sheet_date WHERE time_sheets.time_sheet_date IN (SELECT time_sheet_entries.time_sheet_date FROM time_sheet_entries WHERE time_sheet_entries.rowid = @rowid)";
    }

    public static SqliteCommand ByTimeSheetDate(TrackedDate date)
    {
        var command = new SqliteCommand(CommandTextValues.ByTimeSheetDate);

        command.Parameters.AddRange(new ByTimeSheetDate(date));

        return command;
    }

    public static SqliteCommand ByTimeSheetEntryId(TimeSheetEntryId id)
    {
        var command = new SqliteCommand(CommandTextValues.ByTimeSheetEntryId);

        command.Parameters.AddRange(new ByTimeSheetEntryId(id));

        return command;
    }
}
