using Microsoft.Data.Sqlite;

internal class DeleteTimeSheetEntries
{
    internal class CommandTextValues
    {
        public static readonly string ByTimeSheetEntryId = "DELETE FROM time_sheet_entries WHERE time_sheet_entries.rowid = @rowid";
    }

    public static SqliteCommand ByTimeSheetEntryId(TimeSheetEntryId id)
    {
        var command = new SqliteCommand(CommandTextValues.ByTimeSheetEntryId);

        command.Parameters.AddRange(new ByTimeSheetEntryId(id));

        return command;
    }
}
