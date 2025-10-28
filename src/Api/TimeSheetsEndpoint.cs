internal static class TimeSheetsEndpoint
{
    public static async Task<IResult> GetAsync(ITimeSheets timeSheets, TrackedDate date) =>
        await timeSheets.FindAsync(date) is { } timeSheet ? Results.Ok(timeSheet.ToResource()) : Results.NotFound();
}
