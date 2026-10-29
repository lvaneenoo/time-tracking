namespace TestFloor;

internal class DeleteTimeSheetEntry(TrackedDate date, Period period) : IStorageCommand
{
    private readonly Period _period = period;
    private readonly TrackedDate _date = date;

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

        return Task.FromResult(0);
    }

    private int GetNumber(TimeSheet sheet) => sheet.Entries.Any(entry => _period == entry.Period) ? 1 : 0;
}
