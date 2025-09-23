internal static class PostTimeSheetEntryFeature
{
    public static void AddPostTimeSheetEntryHandler(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/time-sheets/{date}/entries", PostAsync);
    }

    private static async Task<IResult> PostAsync(ITimeSheets timeSheets, string date, TimeSheetEntryPosted body)
    {
        try
        {
            var command = new PostTimeSheetEntry(timeSheets, date, body);

            return await command.ExecuteAsync();
        }
        catch
        {
            return TypedResults.InternalServerError();
        }
    }
}
