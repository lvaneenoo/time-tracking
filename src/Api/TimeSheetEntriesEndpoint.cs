internal static class TimeSheetEntriesEndpoint
{
    public static async Task<IResult> PostAsync(ITimeSheets timeSheets, TrackedDate date, TimeSheetEntryResource body)
    {
        if (!body.TryGetPeriod(out var period))
        {
            return Results.BadRequest();
        }

        if (await timeSheets.FindAsync(date) is not { } timeSheet)
        {
            return Results.NotFound();
        }

        return timeSheet.TryAddEntry(period, out _) ? Results.Created() : Results.BadRequest();
    }
}
