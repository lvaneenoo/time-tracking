using System.Text.Json;

internal static class PostTimeSheetEntryFeature
{
    public static void AddPostTimeSheetEntryHandler(this IEndpointRouteBuilder app)
    {
        app.MapPost("/time-sheets/{date}/entries", async (HttpContext context, string date) =>
        {
            try
            {
                if (!TrackedDate.TryParse(date, out var timeSheetDate))
                {
                    return Results.BadRequest();
                }

                if (await GetBodyAsync(context) is not TimeSheetEntryPosted entryPosted)
                {
                    return Results.BadRequest();
                }

                var command = new AddTimeSheetEntry(context, timeSheetDate, entryPosted);

                return await command.ExecuteAsync();
            }
            catch
            {
                return Results.InternalServerError();
            }
        });
    }

    private static async Task<TimeSheetEntryPosted?> GetBodyAsync(HttpContext context)
    {
        using var reader = new StreamReader(context.Request.Body);

        return JsonSerializer.Deserialize<TimeSheetEntryPosted>(await reader.ReadToEndAsync(context.RequestAborted));
    }
}
