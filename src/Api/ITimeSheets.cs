internal interface ITimeSheets
{
    Task<TimeSheet?> FindAsync(TrackedDate date);
}
