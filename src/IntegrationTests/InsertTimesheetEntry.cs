namespace TestFloor;

internal class InsertTimesheetEntry : IStorageCommand
{
    private readonly Period _period;
    private readonly TrackedDate _date;

    public InsertTimesheetEntry(TrackedDate date, Period period, Comment comment)
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

    private int GetNumber(Timesheet timesheet) => timesheet.Entries.All(entry => _period != entry.Period) ? 1 : 0;
}
