internal class TimeSheetsStub : ITimeSheets
{
    public Task<TimeSheet?> FindAsync(TrackedDate date) => date.IsWeekend()
        ? Task.FromResult<TimeSheet?>(null)
        : Task.FromResult<TimeSheet?>(TimeSheet.Create(date, [CreateEntry1(), CreateEntry2()]));

    private static TimeSheetEntry CreateEntry1() => new(Period.Create(new TimeOnly(9, 0), new TimeOnly(9, 59)));
    private static TimeSheetEntry CreateEntry2() => new(Period.Create(new TimeOnly(10, 0), new TimeOnly(10, 59)));
}
