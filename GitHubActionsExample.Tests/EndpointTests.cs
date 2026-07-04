using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GitHubActionsExample.Tests;

// Uygulamayı bellek içinde ayağa kaldırıp gerçek endpoint'i test eder.
// WebApplicationFactory<Program>, Program.cs'teki minimal API uygulamasını kullanır.
public class EndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public EndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Root_Returns200AndHelloWorld()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Hello World!", body);
    }
}
