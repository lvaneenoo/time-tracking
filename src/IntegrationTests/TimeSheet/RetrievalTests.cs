using System.Net;

namespace IntegrationTests;

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
        using var client = _timeTracking.CreateClient();
        var sender = new GetTimeSheetSender(client);

        return await sender.SendAsync(date, resourceId);
    }
}
