using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;

namespace IntegrationTests;

public class CreationTests(TimeTracking timeTracking) : IClassFixture<TimeTracking>
{
    private static readonly MediaTypeHeaderValue ApplicationJson = new(MediaTypeNames.Application.Json);

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
        var response = await PostAsync("2025-01-01", "09:00", "16:59", "");

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
        var response = await PostAsync("2025-01-02", "09:00", "16:59", "");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static StringContent CreateContent(PostTimeSheetEntryRequest request)
    {
        return new StringContent(JsonSerializer.Serialize(request), ApplicationJson);
    }

    private async Task<HttpResponseMessage> PostAsync(
        string date,
        string periodStart,
        string periodEnd,
        string comment)
    {
        var request = new PostTimeSheetEntryRequest
        {
            Comment = comment,
            Date = date,
            Period = new PeriodResource
            {
                End = periodEnd,
                Start = periodStart
            }
        };

        using var client = _timeTracking.CreateClient();

        return await client.PostAsync(new Uri("/time-sheet-entries/"), CreateContent(request));
    }
}
