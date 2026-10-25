using System.Net;
using Microsoft.Net.Http.Headers;

public class RetrievalTests(TimeTracking timeTracking) : IClassFixture<TimeTracking>
{
    private readonly TimeTracking _timeTracking = timeTracking;

    [Fact]
    public async Task Returns_BadRequest()
    {
        var response = await GetAsync("A");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Returns_NotFound()
    {
        var response = await GetAsync("2025-01-02");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Returns_NotModified()
    {
        var sheet = January2025.First.NoEntries();

        var response = await GetAsync("2025-01-01", sheet.CreateResourceId());

        Assert.Equal(HttpStatusCode.NotModified, response.StatusCode);
    }

    [Fact]
    public async Task Returns_OK()
    {
        var response = await GetAsync("2025-01-01");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private async Task<HttpResponseMessage> GetAsync(string date, string? resourceId = null)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/time-sheets/{date}")
        };

        if (resourceId is not null)
        {
            request.Headers.Add(HeaderNames.IfNoneMatch, $"\"{resourceId}\"");
        }

        using var client = _timeTracking.CreateClient();

        return await client.SendAsync(request);
    }
}
