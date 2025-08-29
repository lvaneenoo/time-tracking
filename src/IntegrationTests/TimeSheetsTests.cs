using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

public class TimeSheetsTests
{
    public static TheoryData<DateOnly> TestData => new()
    {
        { DateOnly.FromDateTime(DateTime.Now) }
    };

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetAsync(DateOnly date)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync(BuildUri(date));

        Assert.True(response.IsSuccessStatusCode);
    }

    private static Uri BuildUri(DateOnly date) => new($"/time-sheets/{date:yyyy-MM-dd}");
}
