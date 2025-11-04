using System.Data.Common;

internal class TimeSheetMaterializer
{
    public static async IAsyncEnumerable<TimeSheet> Materialize(DbDataReader reader)
    {
        if (!reader.HasRows)
        {
            yield break;
        }

        await reader.ReadAsync();

        var dateValue = reader.GetDateTime(0);
        var status = (TimeSheetStatus)reader.GetInt32(1);
        var entries = new List<TimeSheetEntry>();

        if (reader.ToTimeSheetEntry() is { } firstEntry)
        {
            entries.Add(firstEntry);
        }

        while (await reader.ReadAsync())
        {
            if (reader.GetDateTime(0) != dateValue)
            {
                yield return new TimeSheet(new TrackedDate(DateOnly.FromDateTime(dateValue)), entries, status);

                dateValue = reader.GetDateTime(0);
                status = (TimeSheetStatus)reader.GetInt32(1);
                entries = [];
            }

            if (reader.ToTimeSheetEntry() is { } entry)
            {
                entries.Add(entry);
            }
        }

        yield return new TimeSheet(new TrackedDate(DateOnly.FromDateTime(dateValue)), entries, status);
    }
}
