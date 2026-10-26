using Microsoft.Net.Http.Headers;

namespace IntegrationTests;

internal class GetTimeSheetSender(HttpClient client)
{
    private readonly HttpClient _httpClient = client;

    public async Task<HttpResponseMessage> SendAsync(string date, string? resourceId = null)
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

        return await _httpClient.SendAsync(request);
    }
}
