using System.Net;
using System.Text;
using System.Text.Json;
using Common.DAO;
using Common.Request;
using FluentAssertions;
using HealthIndicators;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Tests;
public class UserControllerTests
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ITestOutputHelper _output;

    public UserControllerTests(ITestOutputHelper output) {
        _client = (new WebApplicationFactory<Program>()).CreateClient();
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _output = output;
    }
    
    private async Task<HttpResponseMessage> CreateUser(UserCreationRequest data) {
        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json"
        );

        return await _client.PostAsync("/api/user/create", content);
    }

    [Fact]
    public async Task ShouldGet200_GET_AllUsers() {
        var response = await _client.GetAsync("/api/user/getUsers");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var data = JsonSerializer.Deserialize<IEnumerable<UserDAO>>(
            await response.Content.ReadAsStringAsync(),
            _jsonOptions
        );
        data.Should().NotBeEmpty(); 
    }

    [Theory]
    [InlineData(15, HttpStatusCode.OK)]      
    [InlineData(999, HttpStatusCode.NotFound)] 
    public async Task ShouldGetRelevantHttpCode_GET_UserById(int id, HttpStatusCode expectedStatusCode)
    {
        var response = await _client.GetAsync($"/api/user/getUser/{id}");
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var data = JsonSerializer.Deserialize<UserDAO>(
                await response.Content.ReadAsStringAsync(),
                _jsonOptions
            );

            data.Should().NotBeNull(); 
            data.Id.Should().Be(id);  
        }
    }
    
    [Fact]
    public async Task ShouldConvertWeightToKg()
    {
        string nameTest = "test";
        
        var data = new UserCreationRequest {
            Name = nameTest,
            Age = 52,
            Weight = 78f,
            UnitWeight = "lb",
            Height = 1.52f,
            Password = "password"
        };
        
        var searchResponse = await _client.GetAsync($"/api/user/getUserByName/{nameTest}");
        if (searchResponse.StatusCode == HttpStatusCode.OK) {
            var search = JsonSerializer.Deserialize<UserDAO>(
                await searchResponse.Content.ReadAsStringAsync(),
                _jsonOptions
            );
            
            var response = await _client.DeleteAsync($"/api/user/remove/{search.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        var response1 = await CreateUser(data);
        response1.StatusCode.Should().Be(HttpStatusCode.Created);
       
        var responseData = JsonSerializer.Deserialize<UserDAO>(
            await response1.Content.ReadAsStringAsync(),
            _jsonOptions
        );
        responseData.Weight.Should().Be(35.38025f);
        
        string nameTest2 = "test2";
        
        var data1 = new UserCreationRequest {
            Name = nameTest2,
            Age = 52,
            Weight = 50f,
            UnitWeight = "kg",
            Height = 1.52f,
            Password = "password"
        };
        
        var searchResponse1 = await _client.GetAsync($"/api/user/getUserByName/{nameTest2}");
        if (searchResponse1.StatusCode == HttpStatusCode.OK) {
            var search = JsonSerializer.Deserialize<UserDAO>(
                await searchResponse1.Content.ReadAsStringAsync(),
                _jsonOptions
            );
            
            var response = await _client.DeleteAsync($"/api/user/remove/{search.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        var response2 = await CreateUser(data1);
        response2.StatusCode.Should().Be(HttpStatusCode.Created);
        var responseData2 = JsonSerializer.Deserialize<UserDAO>(
            await response2.Content.ReadAsStringAsync(),
            _jsonOptions
        );
        responseData2.Weight.Should().Be(50f);
    }
}
