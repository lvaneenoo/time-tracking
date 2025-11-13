using System.Net;

namespace IntegrationTests;

public class PostTimeSheetEntryTests
{
    [Theory]
    [ClassData(typeof(WorkedHours))]
    public async Task Post_existing_entry_returns_conflict(DateOnly date, TimeOnly periodStart, TimeOnly periodEnd)
    {
        Assert.Equal(HttpStatusCode.Conflict, await PostAsync(date, periodStart, periodEnd));
    }

    [Theory]
    [ClassData(typeof(WeekdayHours))]
    public async Task Post_existing_time_sheet_returns_created(DateOnly date, TimeOnly periodStart, TimeOnly periodEnd)
    {
        Assert.Equal(HttpStatusCode.Created, await PostAsync(date, periodStart, periodEnd));
    }

    [Theory]
    [ClassData(typeof(WeekendDayHours))]
    public async Task Post_non_existent_time_sheet_returns_not_found(DateOnly date,
                                                                     TimeOnly periodStart,
                                                                     TimeOnly periodEnd)
    {
        Assert.Equal(HttpStatusCode.NotFound, await PostAsync(date, periodStart, periodEnd));
    }

    private static async Task<HttpStatusCode> PostAsync(DateOnly date, TimeOnly periodStart, TimeOnly periodEnd)
    {
        var post = new PostTimeSheetEntry(date, periodStart, periodEnd);

        return await post.ExecuteAsync();
    }
}
