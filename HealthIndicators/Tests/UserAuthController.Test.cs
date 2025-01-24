using System.Text;
using System.Text.Json;
using Common.Request;
using HealthIndicators;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Tests;
public class UserAuthControllerTests
{
    private static readonly List<string> Names = new List<string> { "Alice", "Bob", "Charlie", "Diana", "Edward", "Fiona", "George", "Hannah" };

    private static readonly Random Random = new Random();

    public static string GetRandomName()
    {
        int index = Random.Next(Names.Count);
        return Names[index];
    }
    public HttpClient Client { get; }
    private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };

    public UserAuthControllerTests(ITestOutputHelper output)
    {
        var webApplicationFactory = new WebApplicationFactory<Program>();
        Client = webApplicationFactory.CreateClient();
    }
    
    private async Task<HttpResponseMessage> Login(LoginRequest data) {
        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json"
        );

        return await Client.PostAsync("/login", content);
    }
    
}
