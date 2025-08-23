using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

public class HelloWorldTests
{
    [Fact]
    public async Task GetAsync()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync(new Uri("/hello-world"));

        Assert.True(response.IsSuccessStatusCode);
    }
}
