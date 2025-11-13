using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

internal class PostTimeSheetEntry(DateOnly date, TimeOnly periodStart, TimeOnly periodEnd)
{
    private const string DateFormat = "yyyy-MM-dd";
    private const string JsonMediaType = "application/json";
    private const string TimeFormat = "HH:mm";
    private const string TimeSheetEntries = "/time-sheet-entries";

    private readonly DateOnly _date = date;
    private readonly TimeOnly _periodEnd = periodEnd, _periodStart = periodStart;

    public async Task<HttpStatusCode> ExecuteAsync()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        dynamic request = new
        {
            Date = _date.ToString(DateFormat),
            Period = new
            {
                End = _periodEnd.ToString(TimeFormat),
                Start = _periodStart.ToString(TimeFormat)
            }
        };

        var response = await client.PostAsync(new Uri(TimeSheetEntries), CreateContent(request));

        return response.StatusCode;
    }

    private static StringContent CreateContent(dynamic request) =>
        new(JsonSerializer.Serialize(request), new MediaTypeHeaderValue(JsonMediaType));
}
