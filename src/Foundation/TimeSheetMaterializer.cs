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
            var d = _reader.GetDate();

            if (d != date)
            {
                yield return new TimeSheetSnapshot(date, entries, status)
                {
                    ModifiedOn = modifiedOn
                };

                date = d;
                status = _reader.GetStatus();
                entries = [];

                modifiedOn = _reader.GetModifiedOn();
            }

            if (_reader.ToTimeSheetEntry() is { } entry)
            {
                entries.Add(entry);
            }
        }

        yield return new TimeSheetSnapshot(date, entries, status)
        {
            ModifiedOn = modifiedOn
        };
    }
}
