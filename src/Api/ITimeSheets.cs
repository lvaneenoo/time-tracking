internal interface ITimeSheets
{
    Task<TimeSheet?> FindAsync(TimeSheetEntryId id, CancellationToken cancellationToken = default);
    Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default);
}
