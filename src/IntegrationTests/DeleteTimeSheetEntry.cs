using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

internal class DeleteTimeSheetEntry(long id)
{
    private const string TimeSheetEntries = "time-sheet-entries";

    private readonly long _id = id;

    public async Task<HttpStatusCode> ExecuteAsync()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.DeleteAsync(new Uri($"/{TimeSheetEntries}/{_id}"));

        return response.StatusCode;
    }
}
