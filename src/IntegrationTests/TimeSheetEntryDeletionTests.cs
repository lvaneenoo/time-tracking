using System.Net;
using System.Web;

namespace IntegrationTests;

public class TimeSheetEntryDeletionTests(TimeTrackingFactory factory) : IClassFixture<TimeTrackingFactory>
{
    private readonly TimeTrackingFactory _factory = factory;

    [Fact]
    public async Task ReturnsNotFoundAsync()
    {
        var response = await DeleteAsync("2025-01-01", "08:00", "08:59");

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

        using var client = _factory.CreateClient();

        return await client.DeleteAsync(builder.Uri);
    }
}
