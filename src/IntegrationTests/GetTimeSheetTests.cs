using System.Net;
using Microsoft.Net.Http.Headers;

public class GetTimeSheetTests(TimeTrackingFactory factory) : IClassFixture<TimeTrackingFactory>
{
    private readonly TimeTrackingFactory _factory = factory;

    [Fact]
    public async Task ReturnsNotModifiedAsync()
    {
        var date = new TrackedDate(new DateOnly(2025, 1, 1));
        var sheet = date.Resolve();

        var response = await GetTimeSheetAsync(sheet, sheet.CreateResourceId());

        Assert.Equal(HttpStatusCode.NotModified, response.StatusCode);
    }

    [Fact]
    public async Task ReturnsResourceAsync()
    {
        var date = new TrackedDate(new DateOnly(2025, 1, 1));

        var response = await GetTimeSheetAsync(date.Resolve());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private async Task<HttpResponseMessage> GetTimeSheetAsync(TimeSheet sheet, string? resourceId = null)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = sheet.Resolve()
        };

        if (resourceId is not null)
        {
            request.Headers.Add(HeaderNames.IfNoneMatch, $"\"{resourceId}\"");
        }

        using var client = _factory.CreateClient();

        return await client.SendAsync(request);
    }
}
