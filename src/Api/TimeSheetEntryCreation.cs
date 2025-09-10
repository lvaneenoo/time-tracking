internal static class TimeSheetEntryCreation
{
    public static void MapTimeSheetEntryCreation(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/time-sheets/{s}/entries", PostAsync);
    }

    private static Period CreatePeriod() => Period.Create(new TimeOnly(9, 0), new TimeOnly(9, 59));

    private static async Task<IResult> PostAsync(ITimeSheets timeSheets, string s)
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

        _ = timeSheet.AddEntry(CreatePeriod());

        return TypedResults.Created();
    }
}
