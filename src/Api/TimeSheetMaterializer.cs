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
            var dateCandidate = reader.GetDateTime(0);

            if (dateCandidate != dateValue)
            {
                yield return Create(dateValue, entries, status);

                dateValue = dateCandidate;
                status = (TimeSheetStatus)reader.GetInt32(1);
                entries = [];
            }

            if (reader.ToTimeSheetEntry() is { } entry)
            {
                entries.Add(entry);
            }
        }

        yield return Create(dateValue, entries, status);
    }

    private static TimeSheet Create(DateTime dateValue, List<TimeSheetEntry> entries, TimeSheetStatus status) =>
        new(new TrackedDate(DateOnly.FromDateTime(dateValue)), entries, status);
}
