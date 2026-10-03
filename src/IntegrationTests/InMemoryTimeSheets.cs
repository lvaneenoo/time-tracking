internal class InMemoryTimeSheets(IEnumerable<TimeSheet> sheets) : ITimeSheets
{
    private readonly Dictionary<TrackedDate, TimeSheet> _sheets = sheets.ToDictionary(sheet => sheet.Date);

    public async Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default)
    {
        if (_sheets.TryGetValue(date, out var sheet))
        {
            return await Task.FromResult(sheet);
        }

        return null;
    }
}
