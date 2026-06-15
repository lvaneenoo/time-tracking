internal class InMemoryRepository : ITimeSheets
{
    public async Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(date.Resolve());
    }
}
