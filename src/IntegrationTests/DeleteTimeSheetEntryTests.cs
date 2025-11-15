using System.Net;

namespace IntegrationTests;

public class DeleteTimeSheetEntryTests
{
    [Theory]
    [InlineData(1)]
    public async Task Delete_returns_no_content(long id)
    {
        Assert.Equal(HttpStatusCode.NoContent, await DeleteAsync(id));
    }

    private static async Task<HttpStatusCode> DeleteAsync(long id)
    {
        var delete = new DeleteTimeSheetEntry(id);

        return await delete.ExecuteAsync();
    }
}
