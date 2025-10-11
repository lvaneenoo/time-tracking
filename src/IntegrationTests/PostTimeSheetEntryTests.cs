using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

public class PostTimeSheetEntryTests
{
    [Theory]
    [ClassData(typeof(WorkedHours))]
    public async Task Post_existing_entry_returns_bad_request(DateOnly date, TimeOnly start, TimeOnly end)
    {
        Assert.Equal(HttpStatusCode.BadRequest, await PostAsync(date, start, end));
    }

    [Theory]
    [ClassData(typeof(WeekdayHours))]
    public async Task Post_existing_time_sheet_returns_created(DateOnly date, TimeOnly start, TimeOnly end)
    {
        Assert.Equal(HttpStatusCode.Created, await PostAsync(date, start, end));
    }

    [Theory]
    [ClassData(typeof(WeekendDayHours))]
    public async Task Post_non_existent_time_sheet_returns_not_found(DateOnly date, TimeOnly start, TimeOnly end)
    {
        Assert.Equal(HttpStatusCode.NotFound, await PostAsync(date, start, end));
    }

    private static Uri BuildUri(DateOnly date) => new($"/time-sheets/{date:yyyy-MM-dd}/entries");

    private static StringContent CreateContent(TimeOnly start, TimeOnly end)
    {
        dynamic entry = new
        {
            Start = start.ToString("HH:mm"),
            End = end.ToString("HH:mm")
        };

        return new StringContent(JsonSerializer.Serialize(entry), new MediaTypeHeaderValue("application/json"));
    }

    private static async Task<HttpStatusCode> PostAsync(DateOnly date, TimeOnly start, TimeOnly end)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsync(BuildUri(date), CreateContent(start, end));

        return response.StatusCode;
    }
}
