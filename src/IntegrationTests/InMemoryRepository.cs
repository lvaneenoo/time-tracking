internal class InMemoryRepository : ITimeSheets
{
    public async Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default)
    {
        if (date == January2025.First)
        {
            return await Task.FromResult(date.CreateTimeSheet());
        }

        return null;
    }
}
