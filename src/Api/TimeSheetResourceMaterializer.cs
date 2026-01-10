using Microsoft.Data.Sqlite;

internal class TimeSheetResourceMaterializer(SqliteDataReader reader) : IAsyncEnumerable<TimeSheetResource>
{
    private const string DateFormat = "yyyy-MM-dd";

    private readonly SqliteDataReader _reader = reader;

    public async IAsyncEnumerator<TimeSheetResource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (!_reader.HasRows)
        {
            yield break;
        }

        await _reader.ReadAsync(cancellationToken);

        var date = _reader.GetDateTime(0);
        var status = _reader.GetInt32(1);
        var entries = new List<TimeSheetEntryResource>();

        if (_reader.ToEntryResource() is { } firstEntry)
        {
            entries.Add(firstEntry);
        }

        while (await _reader.ReadAsync(cancellationToken))
        {
            var dateCandidate = _reader.GetDateTime(0);

            if (dateCandidate != date)
            {
                yield return new TimeSheetResource
                {
                    Date = date.ToString(DateFormat),
                    Entries = [.. entries],
                    Status = status
                };

                date = dateCandidate;
                status = _reader.GetInt32(1);
                entries = [];
            }

            if (_reader.ToEntryResource() is { } entry)
            {
                entries.Add(entry);
            }
        }

        yield return new TimeSheetResource
        {
            Date = date.ToString(DateFormat),
            Entries = [.. entries],
            Status = status
        };
    }
}
