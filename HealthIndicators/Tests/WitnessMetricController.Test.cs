using System.Net;
using System.Text;
using System.Text.Json;
using Common.DTO;
using Common.Request;
using Common.Response;
using FluentAssertions;
using HealthIndicators;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Tests;
public class WellnessMetricsControllerTests
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public WellnessMetricsControllerTests() {
        _client = (new WebApplicationFactory<Program>()).CreateClient();
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
    }
    
    private async Task<HttpResponseMessage> CreateWellnessMetrics(WellnessMetricsCreationRequest data) {
        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json"
        );
        
        return await _client.PostAsync("api/metrics/create", content);
    }
    
    [Fact]
    public async Task ShouldGet201_POST_Create() {
        var data = new WellnessMetricsCreationRequest {
            IdUser = 18,
            Steps = 1000,
            SleepDuration = 8,
            HeartRate = 60
        };

        var response = await CreateWellnessMetrics(data);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task Status400BadRequest() {
        var data = new WellnessMetricsCreationRequest {
            IdUser = -1,
            Steps = 1000,
            SleepDuration = 8,
            HeartRate = 60
        };

        var response = await CreateWellnessMetrics(data);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task ShouldGet200_GET_OneMetricById() {
        var response = await _client.GetAsync("api/metrics/getMetric/3");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = JsonSerializer.Deserialize<WellnessMetricsDTO>(
            await response.Content.ReadAsStringAsync(),
            _jsonOptions
        );
        data.Should().NotBeNull();
    }
    
    [Fact]
    public async Task ShouldGet404_GET_OneMetricById() {
        var response = await _client.GetAsync("api/metrics/getMetric/1000");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task TestDistanceValue() {
        var data = new WellnessMetricsCreationRequest
        {
            IdUser = 21,
            Steps = 5821,
            SleepDuration = 8,                                                       
            HeartRate = 60
        };
        
        var response = await CreateWellnessMetrics(data);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseData = JsonSerializer.Deserialize<WellnessMetricsDAO>(
            await response.Content.ReadAsStringAsync(),
            _jsonOptions
        );
        responseData.Should().NotBeNull();
        int id = responseData.Id;

        var response1 = await _client.GetAsync("api/metrics/getMetric/" + id);
        response1.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData1 = JsonSerializer.Deserialize<WellnessMetricsResponse>(
            await response1.Content.ReadAsStringAsync(),
            _jsonOptions
        );
        responseData1.Distance.Should().Be(4.058401f);
    }
    
    [Theory] 
    [InlineData (2, "miles", HttpStatusCode.OK)]
    [InlineData(200, "km", HttpStatusCode.NotFound)]
    public async Task TestDistanceUnit(int id, string unit, HttpStatusCode expectedStatusCode) {  
        string url = $"api/metrics/getMetric/{id}"; 
        var response = await _client.GetAsync(url);            
        response.StatusCode.Should().Be(expectedStatusCode); 
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task TestBmiValue() {
        var data = new WellnessMetricsCreationRequest
        {
            IdUser = 30,
            Steps = 5821,
            SleepDuration = 8,                                                       
            HeartRate = 60
        };
        
        var response = await CreateWellnessMetrics(data);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseData = JsonSerializer.Deserialize<WellnessMetricsDAO>(
            await response.Content.ReadAsStringAsync(),
            _jsonOptions
        );
        responseData.Should().NotBeNull();
        int id = responseData.Id;

        var response1 = await _client.GetAsync("api/metrics/getMetric/" + id);
        response1.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData1 = JsonSerializer.Deserialize<WellnessMetricsResponse>(
            await response1.Content.ReadAsStringAsync(),
            _jsonOptions
        );
        responseData1.Bmi.Should().Be(19.993078f);
    }
}                                                                              
                                                                    
                                                                                   