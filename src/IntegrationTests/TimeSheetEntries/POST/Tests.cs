using System.Net;
using TestFloor;

namespace TimeSheetEntryTests.Creation;

public class Tests(TimeTracking timeTracking) : IClassFixture<TimeTracking>
{
    private readonly TimeTracking _timeTracking = timeTracking;

    [Fact]
    public async Task Returns_BadRequest()
    {
        var response = await PostAsync("", "", "", "");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Returns_Conflict()
    {
        var response = await PostAsync("2025-01-02", "09:00", "16:59", "");

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Returns_Created()
    {
        var response = await PostAsync("2025-01-01", "17:00", "17:59", "");

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Returns_NotFound()
    {
        var response = await PostAsync("2025-01-03", "09:00", "16:59", "");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task<HttpResponseMessage> PostAsync(
        string date,
        string periodStart,
        string periodEnd,
        string comment)
    {
        using var client = _timeTracking.CreateClient();
        var sender = new Sender(client);

        return await sender.SendAsync(date, periodStart, periodEnd, comment);
    }
}
