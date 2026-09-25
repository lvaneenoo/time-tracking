using System.Net;
using Microsoft.Net.Http.Headers;

public class GetTimeSheetTests(TimeTrackingFactory factory) : IClassFixture<TimeTrackingFactory>
{
    private readonly TimeTrackingFactory _factory = factory;

    [Fact]
    public async Task Returns_NotFound()
    {
        var response = await GetAsync(January2025.Second);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Returns_NotModified()
    {
        var sheet = January2025.First.CreateTimeSheet();

        var response = await GetAsync(January2025.First, sheet.CreateResourceId());

        Assert.Equal(HttpStatusCode.NotModified, response.StatusCode);
    }

    [Fact]
    public async Task Returns_OK()
    {
        var response = await GetAsync(January2025.First);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private async Task<HttpResponseMessage> GetAsync(TrackedDate date, string? resourceId = null)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/time-sheets/{date:yyyy-MM-dd}")
        };

        if (resourceId is not null)
        {
            request.Headers.Add(HeaderNames.IfNoneMatch, $"\"{resourceId}\"");
        }

        using var client = _factory.CreateClient();

        return await client.SendAsync(request);
    }
}
