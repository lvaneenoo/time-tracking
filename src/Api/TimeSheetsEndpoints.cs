using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

internal static class TimeSheetsEndpoints
{
    public static async Task<IResult> GetAsync(
        TrackedDate date,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        var sheets = context.RequestServices.GetRequiredService<ITimeSheets>();

        if (await sheets.FindAsync(date, cancellationToken) is not { } sheet)
        {
            return Results.NotFound();
        }

        var resourceId = $"\"{sheet.CreateResourceId()}\"";

        if (context.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var value) && value == resourceId)
        {
            return Results.StatusCode(304);
        }

        context.Response.Headers.ETag = new StringValues(resourceId);

        return Results.Ok(sheet.ToResource());
    }
}
