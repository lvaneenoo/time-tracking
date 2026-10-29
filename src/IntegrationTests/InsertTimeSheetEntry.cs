namespace TestFloor;

internal class InsertTimeSheetEntry : IStorageCommand
{
    private readonly Period _period;
    private readonly TrackedDate _date;

    public InsertTimeSheetEntry(TrackedDate date, Period period, Comment comment)
    {
        _date = date;
        _period = period;
        _ = comment;
    }

    public Task<int> ExecuteAsync(CancellationToken cancellationToken)
    {
        if (_date == January2025.First.Date)
        {
            return Task.FromResult(GetNumber(January2025.First));
        }

        if (_date == January2025.Second.Date)
        {
            return Task.FromResult(GetNumber(January2025.Second));
        }

        throw new InvalidOperationException();
    }

    private int GetNumber(TimeSheet sheet) => sheet.Entries.All(entry => _period != entry.Period) ? 1 : 0;
}
