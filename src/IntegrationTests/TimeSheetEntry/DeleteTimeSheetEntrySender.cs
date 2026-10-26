using System.Web;

namespace IntegrationTests;

internal class DeleteTimeSheetEntrySender(HttpClient client)
{
    private readonly HttpClient _httpClient = client;

    public async Task<HttpResponseMessage> SendAsync(string date, string periodStart, string periodEnd)
    {
        var builder = new UriBuilder("http://localhost/time-sheet-entries/");
        var query = HttpUtility.ParseQueryString("");

        query[ParameterNames.Date] = date;
        query[ParameterNames.PeriodStart] = periodStart;
        query[ParameterNames.PeriodEnd] = periodEnd;

        builder.Query = query.ToString();

        return await _httpClient.DeleteAsync(builder.Uri);
    }

    internal static class ParameterNames
    {
        public static readonly string Date = "date";
        public static readonly string PeriodEnd = "period-end";
        public static readonly string PeriodStart = "period-start";
    }
}
