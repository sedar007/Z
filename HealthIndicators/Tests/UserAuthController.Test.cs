using System.Net;
using System.Text;
using System.Text.Json;
using Common.Request;
using FluentAssertions;
using HealthIndicators;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;



namespace Tests {
    public class UserAuthControllerTests
    {
        
        private static readonly List<string> Names = new List<string>
        {
            "Alice", "Bob", "Charlie", "Diana", "Edward", "Fiona", "George", "Hannah"
        };

        private static readonly Random Random = new Random();

        public static string GetRandomName()
        {
            int index = Random.Next(Names.Count);
            return Names[index];
        }
        public HttpClient Client { get; }
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public UserAuthControllerTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            Client = webApplicationFactory.CreateClient();
        }
        
        
        private async Task<HttpResponseMessage> Create(UserAuthCreationRequest data) {
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            return await Client.PostAsync("/api/admin/create/", content);
        }
        
        [Fact]
        public async void ShouldGet201_POST_Create() {
            var name = GetRandomName();
            var data = new UserAuthCreationRequest {
              Username = name,
                Password = name,
            };

            var response = await Create(data);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
        
        private async Task<HttpResponseMessage> Login(LoginRequest data) {
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            return await Client.PostAsync("/login", content);
        }
        
       
        [Fact]
        public async void ShouldLogin() {
            var name = GetRandomName();
            var data = new UserAuthCreationRequest {
                Username = name,
                Password = name,
            };

            var createResponse = await Create(data);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var loginRequest = new LoginRequest {
                Username = name,
                Password = name,
            };

            var loginResponse = await Login(loginRequest);
            loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        


        
        
    }
}