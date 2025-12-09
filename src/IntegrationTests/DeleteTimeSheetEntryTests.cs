using System.Net;

namespace IntegrationTests;

public class DeleteTimeSheetEntryTests
{
    [Theory]
    [InlineData(3)]
    public async Task Delete_non_existent_entry_returns_not_found(long id)
    {
        Assert.Equal(HttpStatusCode.NotFound, await DeleteAsync(id));
    }

    private static async Task<HttpStatusCode> DeleteAsync(long id)
    {
        var delete = new DeleteTimeSheetEntry(id);

        return await delete.ExecuteAsync();
    }
}
