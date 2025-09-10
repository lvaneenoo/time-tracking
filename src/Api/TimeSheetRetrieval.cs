internal static class TimeSheetRetrieval
{
    public static void MapTimeSheetRetrieval(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/time-sheets/{s}", GetAsync);
    }

    private static async Task<IResult> GetAsync(ITimeSheets timeSheets, string s)
    {
        if (!DateOnly.TryParseExact(s, "yyyy-MM-dd", out DateOnly value))
        {
            return TypedResults.BadRequest();
        }

        if (!TrackedDate.TryCreate(value, out TrackedDate? date))
        {
            return TypedResults.BadRequest();
        }

        if (await timeSheets.FindAsync(date) is not TimeSheet timeSheet)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(timeSheet.Date.ToString());
    }
}
