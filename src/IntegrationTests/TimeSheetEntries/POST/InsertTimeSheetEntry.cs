namespace TimeSheetEntryTests.Creation;

internal class InsertTimeSheetEntry(TrackedDate date, Period period, Comment comment) : IStorageCommand
{
    private static readonly Period FiveToSix = new(new TimeOnly(17, 0), new TimeOnly(17, 59));

    private readonly Comment _comment = comment;
    private readonly Period _period = period;
    private readonly TrackedDate _date = date;

    public Task<int> ExecuteAsync(CancellationToken cancellationToken)
    {
        if (_date == January2025.First && _period == FiveToSix && _comment == Comment.Empty)
        {
            return Task.FromResult(1);
        }

        return Task.FromResult(0);
    }
}
