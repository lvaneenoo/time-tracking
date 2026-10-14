using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

internal static class TimeSheetsEndpoints
{
    public static async Task<IResult> GetAsync(string date, HttpContext context, CancellationToken cancellationToken)
    {
        if (!TrackedDate.TryParse(date, null, out var d))
        {
            return Results.BadRequest();
        }

        var sheets = context.RequestServices.GetRequiredService<ITimeSheets>();

        if (await sheets.FindAsync(d, cancellationToken) is not { } sheet)
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
