internal static class TimeSheetEntriesEndpoint
{
    public static async Task<IResult> PostAsync(ITimeSheets timeSheets, TrackedDate date, PeriodResource body)
    {
        if (body.ToValue() is not { } period)
        {
            return Results.BadRequest();
        }

        if (await timeSheets.FindAsync(date) is not { } timeSheet)
        {
            return Results.NotFound();
        }

        var (_, entry) = timeSheet.AddEntry(period);

        return entry is null ? Results.Conflict() : Results.Created();
    }
}
