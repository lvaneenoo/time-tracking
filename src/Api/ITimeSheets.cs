internal interface ITimeSheets
{
    Task<TimeSheet?> FindAsync(TrackedDate date, CancellationToken cancellationToken = default);
    Task<TimeSheet?> FindAsync(TimeSheetEntryId entryId, CancellationToken cancellationToken = default);
}
