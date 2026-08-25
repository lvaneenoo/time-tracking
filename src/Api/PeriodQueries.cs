using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;

internal static class PeriodQueries
{
    private const string TimeOfDay = "HH:mm";

    public static ReadOnlyCollection<SqliteParameter> Resolve(this Period period)
    {
        return
        [
            new SqliteParameter($"@{TimeSheetEntryTable.PeriodStart}", SqliteType.Text)
            {
                Value = period.Start.ToString(TimeOfDay)
            },
            new SqliteParameter($"@{TimeSheetEntryTable.PeriodEnd}", SqliteType.Text)
            {
                Value = period.End.ToString(TimeOfDay)
            }
        ];
    }

    public static PeriodResource ToResource(this Period period)
    {
        return new()
        {
            End = period.End.ToString(TimeOfDay),
            Start = period.Start.ToString(TimeOfDay)
        };
    }
}
