internal static class TimeSheetEntriesEndpoint
{
    public static Task<IResult> DeleteAsync(TimeSheetEntryId id) => Task.FromResult(Results.NoContent());

    public static async Task<IResult> PostAsync(ITimeSheets timeSheets, PostTimeSheetEntryRequest request)
    {
        if (!TrackedDate.TryParse(request.Date, null, out var date))
        {
            return Results.BadRequest();
        }

        if (request.Period.ToValue() is not { } period)
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
