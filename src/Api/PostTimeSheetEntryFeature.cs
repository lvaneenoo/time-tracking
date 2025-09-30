using System.Text.Json;

internal static class PostTimeSheetEntryFeature
{
    public static void AddPostTimeSheetEntryHandler(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/time-sheets/{date}/entries", PostAsync);
    }

    private static async Task<IResult> PostAsync(HttpContext context, string date)
    {
        try
        {
            if (!TrackedDate.TryParse(date, out var timeSheetDate))
            {
                return TypedResults.BadRequest();
            }

            if (await GetBodyAsync(context) is not TimeSheetEntryPosted entryPosted)
            {
                return TypedResults.BadRequest();
            }

            var command = new AddTimeSheetEntry(ResolveTimeSheets(context), timeSheetDate, entryPosted);

            return await command.ExecuteAsync();
        }
        catch
        {
            return TypedResults.InternalServerError();
        }
    }

    private static async Task<TimeSheetEntryPosted?> GetBodyAsync(HttpContext context)
    {
        using var reader = new StreamReader(context.Request.Body);

        return JsonSerializer.Deserialize<TimeSheetEntryPosted>(await reader.ReadToEndAsync(context.RequestAborted));
    }

    private static ITimeSheets ResolveTimeSheets(HttpContext context) =>
        context.RequestServices.GetRequiredService<ITimeSheets>();
}
