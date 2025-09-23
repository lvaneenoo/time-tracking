using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

public class PostTimeSheetEntryTests
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

    private static StringContent CreateContent()
    {
        dynamic entry = new
        {
            Start = "09:00:00",
            End = "09:59:00"
        };

        return new StringContent(JsonSerializer.Serialize(entry), new MediaTypeHeaderValue("application/json"));
    }

    private static async Task<HttpStatusCode> PostAsync(DateOnly date)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsync(BuildUri(date), CreateContent());

        return response.StatusCode;
    }
}
