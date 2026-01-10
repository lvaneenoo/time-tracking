internal static class TimeSheetsEndpoint
{
    public static async Task<IResult> GetAsync(WriteStore writeStore, TrackedDate date)
    {
        using var command = RetrieveTimeSheets.ByTimeSheetDate(date);
        using var reader = await writeStore.ExecuteReaderAsync(command);

        var materializer = new TimeSheetResourceMaterializer(reader);

        return await materializer.SingleOrDefaultAsync() is { } timeSheet ? Results.Ok(timeSheet) : Results.NotFound();
    }
}
