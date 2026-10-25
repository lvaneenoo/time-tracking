using System.Net;
using System.Web;

namespace IntegrationTests;

public class DeletionTests(TimeTracking timeTracking) : IClassFixture<TimeTracking>
{
    internal static class ParameterNames
    {
        public static readonly string Date = "date";
        public static readonly string PeriodEnd = "period-end";
        public static readonly string PeriodStart = "period-start";
    }

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
        var builder = new UriBuilder("http://localhost/time-sheet-entries/");
        var query = HttpUtility.ParseQueryString("");

        query[ParameterNames.Date] = date;
        query[ParameterNames.PeriodStart] = periodStart;
        query[ParameterNames.PeriodEnd] = periodEnd;

        builder.Query = query.ToString();

        using var client = _timeTracking.CreateClient();

        return await client.DeleteAsync(builder.Uri);
    }
}
