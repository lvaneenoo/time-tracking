using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

public class TimeSheetEntriesTests
{
    [Theory]
    [ClassData(typeof(Weekdays))]
    public async Task Post_existing_time_sheet_returns_created(DateOnly date)
    {
        Assert.Equal(HttpStatusCode.Created, await PostAsync(date));
    }

    [Theory]
    [ClassData(typeof(WeekendDays))]
    public async Task Post_non_existent_time_sheet_returns_not_found(DateOnly date)
    {
        Assert.Equal(HttpStatusCode.NotFound, await PostAsync(date));
    }

    private static Uri BuildUri(DateOnly date) => new($"/time-sheets/{date:yyyy-MM-dd}/entries");

    private static async Task<HttpStatusCode> PostAsync(DateOnly date)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsync(BuildUri(date), null);

        return response.StatusCode;
    }
}
