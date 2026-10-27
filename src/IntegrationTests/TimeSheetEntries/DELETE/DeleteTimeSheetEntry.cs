namespace TimeSheetEntryTests.Deletion;

internal class DeleteTimeSheetEntry(TrackedDate date, Period period) : IStorageCommand
{
    private static readonly Period NineToFive = new(new TimeOnly(9, 0), new TimeOnly(16, 59));

    private readonly Period _period = period;
    private readonly TrackedDate _date = date;

    public Task<int> ExecuteAsync(CancellationToken cancellationToken)
    {
        if (_date == January2025.First && _period == NineToFive)
        {
            return Task.FromResult(1);
        }

        return Task.FromResult(0);
    }
}
