using System.Net;
using System.Web;

using TestFloor;

namespace IntegrationTests;

public class DeletionTests(TimeTrackingFactory factory) : IClassFixture<TimeTrackingFactory>
{
    internal static class ParameterNames
    {
        public static readonly string Date = "date";
        public static readonly string PeriodEnd = "period-end";
        public static readonly string PeriodStart = "period-start";
    }

    private readonly TimeTrackingFactory _factory = factory;

    [Fact]
    public async Task Returns_NoContent()
    {
        var response = await DeleteAsync(January2025.First, Entries.NineToFive[0].Period);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Returns_NotFound()
    {
        var response = await DeleteAsync(January2025.Second, Entries.NineToFive[0].Period);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task<HttpResponseMessage> DeleteAsync(TrackedDate date, Period period)
    {
        var builder = new UriBuilder("http://localhost/time-sheet-entries/");
        var query = HttpUtility.ParseQueryString("");

        query[ParameterNames.Date] = date.ToString("yyyy-MM-dd", null);
        query[ParameterNames.PeriodStart] = period.Start.ToString("HH:mm", null);
        query[ParameterNames.PeriodEnd] = period.End.ToString("HH:mm", null);

        builder.Query = query.ToString();

        using var client = _factory.CreateClient();

        return await client.DeleteAsync(builder.Uri);
    }
}
