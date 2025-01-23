using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Xunit;
using Common.DAO;
using HealthIndicators;

namespace Tests
{
    public class UserControllerTests
    {
        public HttpClient Client { get; }
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public UserControllerTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            Client = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task ShouldGet200_GET_AllUsers()
        {
            // Act
            var response = await Client.GetAsync("/api/User/");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Assert
            var data = JsonSerializer.Deserialize<IEnumerable<UserDAO>>(
                await response.Content.ReadAsStringAsync(),
                jsonOptions
            );

            data.Should().NotBeEmpty(); // Verify if they are users
        }

        [Theory]
        [InlineData(1, HttpStatusCode.OK)]      // existing ID
        [InlineData(999, HttpStatusCode.NotFound)] // not existing ID
        public async Task ShouldGetRelevantHttpCode_GET_UserById(int id, HttpStatusCode expectedStatusCode)
        {
            // Act
            var response = await Client.GetAsync($"/api/User/{id}");
            response.StatusCode.Should().Be(expectedStatusCode);

            if (expectedStatusCode == HttpStatusCode.OK)
            {
                var data = JsonSerializer.Deserialize<UserDAO>(
                    await response.Content.ReadAsStringAsync(),
                    jsonOptions
                );

                data.Should().NotBeNull(); // The user must be found
                data.Id.Should().Be(id);   // he ID must match
            }
        }
    }
}