internal interface ITimeSheets
{
    Task<TimeSheet?> FindAsync(TimeSheetEntryId id);
    Task<TimeSheet?> FindAsync(TrackedDate date);
}
