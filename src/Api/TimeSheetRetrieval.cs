internal static class TimeSheetRetrieval
{
    public static void MapTimeSheetRetrieval(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/time-sheets/{s}", GetAsync);
    }

    private static async Task<IResult> GetAsync(ITimeSheets timeSheets, string s)
    {
        try
        {
            var retrieval = new RetrieveTimeSheet(timeSheets, s);

            return await retrieval.ExecuteAsync();
        }
        catch
        {
            return TypedResults.InternalServerError();
        }
    }
}
