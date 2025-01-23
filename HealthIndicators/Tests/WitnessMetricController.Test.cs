using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Xunit;
using Common.DTO;
using Common.Request;
using HealthIndicators;
using System.Text;

namespace Tests
{
  
    public class WellnessMetricsControllerTests
    {
        public HttpClient Client { get; }
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public WellnessMetricsControllerTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            Client = webApplicationFactory.CreateClient();
        }
        
        
        private async Task<HttpResponseMessage> CreateWellnessMetrics(WellnessMetricsCreationRequest data) {
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            return await Client.PostAsync("/api/metrics/create/", content);
        }
        
        [Fact]
        public async void ShouldGet201_POST_Create() {
            var data = new WellnessMetricsCreationRequest {
                IdUser = 1,
                Steps = 1000,
                SleepDuration = 8,
                HeartRate = 60
            };

            var response = await CreateWellnessMetrics(data);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
        
        [Fact]
        public async void Status400BadRequest() {
            var data = new WellnessMetricsCreationRequest {
                IdUser = -1,
                Steps = 1000,
                SleepDuration = 8,
                HeartRate = 60
            };

            var response = await CreateWellnessMetrics(data);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

          /* [Fact]
        public async Task ShouldGet200_GET_AllWellnessMetrics()
        {
            // Act
            var response = await Client.GetAsync("/api/WellnessMetrics/");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Assert
            var data = JsonSerializer.Deserialize<IEnumerable<WellnessMetricsDAO>>(
                await response.Content.ReadAsStringAsync(),
                jsonOptions
            );

            data.Should().NotBeEmpty(); // Checks that metrics exist on our databas
        }

     [Theory]
        [InlineData(1, HttpStatusCode.OK)]      // existing ID
        [InlineData(999, HttpStatusCode.NotFound)] // not existing ID
        public async Task ShouldGetRelevantHttpCode_GET_WellnessMetricById(int id, HttpStatusCode expectedStatusCode)
        {
            // Act
            var response = await Client.GetAsync($"/api/WellnessMetrics/{id}");
            response.StatusCode.Should().Be(expectedStatusCode);

            if (expectedStatusCode == HttpStatusCode.OK)
            {
                var data = JsonSerializer.Deserialize<WellnessMetricsDAO>(
                    await response.Content.ReadAsStringAsync(),
                    jsonOptions
                );

                data.Should().NotBeNull(); 
                data.Id.Should().Be(id); 
            }
        } */
    }
}
