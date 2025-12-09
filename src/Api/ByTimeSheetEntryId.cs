using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;

internal class ByTimeSheetEntryId(TimeSheetEntryId value) : ReadOnlyCollection<SqliteParameter>(CreateList(value))
{
    private const string RowId = "@rowid";

    private static List<SqliteParameter> CreateList(TimeSheetEntryId value)
    {
        return
        [
            new SqliteParameter(RowId, SqliteType.Integer)
            {
                Value = (long)value
            }
        ];
    }
}
