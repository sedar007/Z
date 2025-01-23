using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Xunit;
using Common.DTO;
using HealthIndicators;

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

        [Fact]
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
        }
    }
}
