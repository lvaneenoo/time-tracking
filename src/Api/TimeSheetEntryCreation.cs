internal static class TimeSheetEntryCreation
{
    public static void MapTimeSheetEntryCreation(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/time-sheets/{s}/entries", PostAsync);
    }

    private static async Task<IResult> PostAsync(ITimeSheets timeSheets, string s)
    {
        try
        {
            var creation = new CreateTimeSheetEntry(timeSheets, s);

            return await creation.ExecuteAsync();
        }
        catch
        {
            return TypedResults.InternalServerError();
        }
    }
}
