using System.Net;

namespace TimeSheetEntryTests.Deletion;

public class Tests(TimeTracking timeTracking) : IClassFixture<TimeTracking>
{
    private readonly TimeTracking _timeTracking = timeTracking;

    [Fact]
    public async Task Returns_BadRequest()
    {
        var response = await DeleteAsync("A", "09:00", "16:59");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Returns_NoContent()
    {
        var response = await DeleteAsync("2025-01-01", "09:00", "16:59");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Returns_NotFound()
    {
        var response = await DeleteAsync("2025-01-02", "09:00", "16:59");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task<HttpResponseMessage> DeleteAsync(string date, string periodStart, string periodEnd)
    {
        using var client = _timeTracking.CreateClient();
        var sender = new Sender(client);

        return await sender.SendAsync(date, periodStart, periodEnd);
    }
}
