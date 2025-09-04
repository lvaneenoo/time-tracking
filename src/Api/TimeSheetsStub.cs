internal class TimeSheetsStub : ITimeSheets
{
    public Task<TimeSheet?> FindAsync(TrackedDate date) => date.IsWeekend()
        ? Task.FromResult<TimeSheet?>(null)
        : Task.FromResult<TimeSheet?>(new TimeSheet(date, []));
}
