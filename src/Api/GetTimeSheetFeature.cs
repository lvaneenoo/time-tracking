internal static class GetTimeSheetFeature
{
    public static void AddGetTimeSheetHandler(this IEndpointRouteBuilder app)
    {
        app.MapGet("/time-sheets/{date}", async (HttpContext context, TrackedDate date) =>
        {
            try
            {
                var timeSheets = context.RequestServices.GetRequiredService<ITimeSheets>();

                if (await timeSheets.FindAsync(date) is not TimeSheet timeSheet)
                {
                    return Results.NotFound();
                }

                return Results.Ok(timeSheet.ToResource());
            }
            catch
            {
                return Results.InternalServerError();
            }
        });
    }
}
