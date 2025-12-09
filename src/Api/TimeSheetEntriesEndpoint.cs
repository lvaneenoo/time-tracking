internal static class TimeSheetEntriesEndpoint
{
    public static async Task<IResult> DeleteAsync(ITimeSheets timeSheets, WriteStore writeStore, TimeSheetEntryId id)
    {
        if (await timeSheets.FindAsync(id) is null)
        {
            return Results.NotFound();
        }

        using var command = DeleteTimeSheetEntries.ByTimeSheetEntryId(id);

        return await writeStore.ExecuteNonQueryAsync(command) switch
        {
            0 => Results.Conflict(),
            1 => Results.NoContent(),
            _ => Results.InternalServerError()
        };
    }

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
