using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;
using Common.DAO;
using Common.Request;
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
        
        private async Task<HttpResponseMessage> CreateUser(UserCreationRequest data) {
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            return await Client.PostAsync("/api/user/create", content);
        }

        [Fact]
        public async Task ShouldGet200_GET_AllUsers()
        {
            // Act
            var response = await Client.GetAsync("/api/User/getUsers");
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
            var response = await Client.GetAsync($"/api/User/getUser/{id}");
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
        
        [Fact]
        public async Task ShouldConvertWeightToKg()
        {
            var data = new UserCreationRequest {
                Name = "User test",
                Age = 52,
                Weight = 50f,
                UnitWeight = "lb",
                Height = 1.52f
            };
            var response = await CreateUser(data);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseData = JsonSerializer.Deserialize<UserDAO>(
                await response.Content.ReadAsStringAsync(),
                jsonOptions
            );
            responseData.Weight.Should().Be(110.230995f);

            var data1 = new UserCreationRequest {
                Name = "User test",
                Age = 52,
                Weight = 50f,
                UnitWeight = "kg",
                Height = 1.52f
            };
            
            var response1 = await CreateUser(data1);
            response1.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseData1 = JsonSerializer.Deserialize<UserDAO>(
                await response1.Content.ReadAsStringAsync(),
                jsonOptions
            );
            responseData1.Weight.Should().Be(50f);
        }
    }
}