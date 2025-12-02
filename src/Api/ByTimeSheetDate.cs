using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;

internal class ByTimeSheetDate(TrackedDate value) : ReadOnlyCollection<SqliteParameter>(CreateList(value))
{
    private const string TimeSheetDate = "@time_sheet_date";

    private static List<SqliteParameter> CreateList(TrackedDate value)
    {
        return
        [
            new SqliteParameter(TimeSheetDate, SqliteType.Text)
            {
                Value = (DateOnly)value
            }
        ];
    }
}
