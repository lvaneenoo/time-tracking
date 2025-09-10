using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

public class TimeSheetsTests
{
    [Theory]
    [ClassData(typeof(Weekdays))]
    public async Task Get_existing_returns_ok(DateOnly date)
    {
        Assert.Equal(HttpStatusCode.OK, await GetAsync(date));
    }

    [Theory]
    [ClassData(typeof(WeekendDays))]
    public async Task Get_non_existent_returns_not_found(DateOnly date)
    {
        Assert.Equal(HttpStatusCode.NotFound, await GetAsync(date));
    }

    private static Uri BuildUri(DateOnly date) => new($"/time-sheets/{date:yyyy-MM-dd}");

    private static async Task<HttpStatusCode> GetAsync(DateOnly date)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync(BuildUri(date));

        return response.StatusCode;
    }
}
