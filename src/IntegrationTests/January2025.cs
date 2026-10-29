namespace TestFloor;

internal class January2025 : ITimeSheets
{
    static January2025()
    {
        var date = new TrackedDate(new DateOnly(2025, 1, 1));
        First = date.NoEntries();

        date = new TrackedDate(new DateOnly(2025, 1, 2));
        Second = date.NineToFive();
    }

    public static TimeSheet First { get; }
    public static TimeSheet Second { get; }

    public async Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default)
    {
        if (date == First.Date)
        {
            return await Task.FromResult(First);
        }

        if (date == Second.Date)
        {
            return await Task.FromResult(Second);
        }

        return null;
    }
}
