internal interface ITimesheets
{
    Task<Timesheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default);
}
