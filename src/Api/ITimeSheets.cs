internal interface ITimeSheets
{
    Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default);
}
