using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

internal class GetTimeSheet(ITimeSheets timeSheets, TrackedDate date, HttpContext httpContext) : IApplicationQuery
{
    private readonly ITimeSheets _timeSheets = timeSheets;

    private readonly HttpContext _httpContext = httpContext;
    private readonly TrackedDate _date = date;

    public async Task<IResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (await _timeSheets.FindAsync(_date, cancellationToken) is not { } sheet)
        {
            return Results.NotFound();
        }

        var resourceId = $"\"{sheet.CreateResourceId()}\"";

        if (_httpContext.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var value) && value == resourceId)
        {
            return Results.StatusCode(304);
        }

        _httpContext.Response.Headers.ETag = new StringValues(resourceId);

        return Results.Ok(sheet.ToResource());
    }
}
