using System.Collections.ObjectModel;

using Microsoft.Data.Sqlite;

internal class ByTimeSheetEntryId : ReadOnlyCollection<SqliteParameter>
{
    private const string RowId = "@rowid";

    private ByTimeSheetEntryId(TimeSheetEntryId value) : base(CreateList(value))
    {
    }

    public static ByTimeSheetEntryId Create(TimeSheetEntryId value) => new(value);

    private static List<SqliteParameter> CreateList(TimeSheetEntryId value) =>
        [
            new(RowId, SqliteType.Integer)
            {
                Value = (long)value
            }
        ];
}
