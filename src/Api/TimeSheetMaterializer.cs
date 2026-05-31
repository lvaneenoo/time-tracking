using Microsoft.Data.Sqlite;

internal class TimeSheetMaterializer(SqliteDataReader reader) : IAsyncEnumerable<TimeSheet>
{
    private readonly SqliteDataReader _reader = reader;

    public async IAsyncEnumerator<TimeSheet> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (!_reader.HasRows)
        {
            yield break;
        }

        await _reader.ReadAsync(cancellationToken);

        var dateValue = _reader.GetDateTime(0);
        var status = (TimeSheetStatus)_reader.GetInt32(1);
        var entries = new List<TimeSheetEntry>();

        var modifiedOn = _reader.GetDateTimeOffset(2);

        if (_reader.ToTimeSheetEntry() is { } firstEntry)
        {
            entries.Add(firstEntry);
        }

        while (await _reader.ReadAsync(cancellationToken))
        {
            var dateCandidate = _reader.GetDateTime(0);

            if (dateCandidate != dateValue)
            {
                yield return new TimeSheetSnapshot(new TimeSheet(dateValue.ToTrackedDate(), entries, status))
                {
                    ModifiedOn = modifiedOn
                };

                dateValue = dateCandidate;
                status = (TimeSheetStatus)_reader.GetInt32(1);
                entries = [];

                modifiedOn = _reader.GetDateTimeOffset(2);
            }

            if (_reader.ToTimeSheetEntry() is { } entry)
            {
                entries.Add(entry);
            }
        }

        yield return new TimeSheetSnapshot(new TimeSheet(dateValue.ToTrackedDate(), entries, status))
        {
            ModifiedOn = modifiedOn
        };
    }
}
