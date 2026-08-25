using Microsoft.Data.Sqlite;

public class TimeSheetMaterializer(SqliteDataReader reader) : IAsyncEnumerable<TimeSheet>
{
    private readonly SqliteDataReader _reader = reader;

    public async IAsyncEnumerator<TimeSheet> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (!_reader.HasRows)
        {
            yield break;
        }

        await _reader.ReadAsync(cancellationToken);

        var date = _reader.GetDate();
        var status = _reader.GetStatus();
        var entries = new List<TimeSheetEntry>();

        var modifiedOn = _reader.GetModifiedOn();

        if (_reader.ToTimeSheetEntry() is { } firstEntry)
        {
            entries.Add(firstEntry);
        }

        while (await _reader.ReadAsync(cancellationToken))
        {
            var dateCandidate = _reader.GetDate();

            if (dateCandidate != date)
            {
                yield return new TimeSheetSnapshot(Create(date, entries, status))
                {
                    ModifiedOn = modifiedOn
                };

                date = dateCandidate;
                status = _reader.GetStatus();
                entries = [];

                modifiedOn = _reader.GetModifiedOn();
            }

            if (_reader.ToTimeSheetEntry() is { } entry)
            {
                entries.Add(entry);
            }
        }

        yield return new TimeSheetSnapshot(Create(date, entries, status))
        {
            ModifiedOn = modifiedOn
        };
    }

    private static TimeSheet Create(DateTime date, IList<TimeSheetEntry> entries, int status)
    {
        return new(date.ToTrackedDate(), entries, (TimeSheetStatus)status);
    }
}
