using System.Collections.ObjectModel;

using Microsoft.Data.Sqlite;

internal class ByTimeSheetDate : ReadOnlyCollection<SqliteParameter>
{
    private const string TimeSheetDate = "@time_sheet_date";

    private ByTimeSheetDate(TrackedDate value) : base(CreateList(value))
    {
    }

    public static ByTimeSheetDate Create(TrackedDate value) => new(value);

    private static List<SqliteParameter> CreateList(TrackedDate value) =>
        [
            new(TimeSheetDate, SqliteType.Text)
            {
                Value = (DateOnly)value
            }
        ];
}
