internal class TimeSheetsStub : ITimeSheets
{
    public Task<TimeSheet> FindAsync(TrackedDate date) => Task.FromResult(new TimeSheet(date));
}
