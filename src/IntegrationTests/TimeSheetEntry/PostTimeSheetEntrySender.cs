using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;

namespace IntegrationTests;

internal class PostTimeSheetEntrySender(HttpClient client)
{
    private static readonly MediaTypeHeaderValue ApplicationJson = new(MediaTypeNames.Application.Json);

    private readonly HttpClient _httpClient = client;

    public async Task<HttpResponseMessage> SendAsync(string date, string periodStart, string periodEnd, string comment)
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

        return await _httpClient.PostAsync(new Uri("/time-sheet-entries/"), CreateContent(request));
    }

    private static StringContent CreateContent(PostTimeSheetEntryRequest request)
    {
        return new StringContent(JsonSerializer.Serialize(request), ApplicationJson);
    }
}
