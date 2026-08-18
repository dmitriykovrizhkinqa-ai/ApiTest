using ApiTestFramework.Core;
using ApiTestFramework.Models;
using ApiTestFramework.Helpers;
using FluentAssertions;

namespace ApiTestFramework.ApiTests
{
    [TestFixture]
    public class UserTests : BaseTest
    {
        private ApiClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new ApiClient(BaseUrl);
        }

        [Test, Category("Smoke")]
        public async Task GetUsers_ShouldReturnSuccess()
        {
            // Arrange
            var endpoint = Endpoints.Users;

            // Act
            var response = await _apiClient.GetAsync<User[]>(endpoint);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data.Length.Should().BeGreaterThan(0);
            
            Logger.LogInfo($"Получено {response.Data.Length} пользователей");
        }

        [Test, Category("Smoke")]
        public async Task GetUserById_ShouldReturnCorrectUser()
        {
            // Arrange
            int userId = 1;
            var endpoint = Endpoints.UserById.Replace("{id}", userId.ToString());

            // Act
            var response = await _apiClient.GetAsync<User>(endpoint);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Id.Should().Be(userId);
            response.Data.Name.Should().NotBeNullOrEmpty();
        }

        [Test, Category("Regression")]
        public async Task CreateUser_ShouldReturnCreated()
        {
            // Arrange
            var newUser = DataGenerator.GetRandomUser();

            // Act
            var response = await _apiClient.PostAsync<User>(Endpoints.Users, newUser);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Data.Should().NotBeNull();
            response.Data.Id.Should().BeGreaterThan(0);
            response.Data.Name.Should().Be(newUser.Name);
            response.Data.Email.Should().Be(newUser.Email);
        }

        [Test, Category("Regression")]
        public async Task UpdateUser_ShouldReturnSuccess()
        {
            // Arrange
            int userId = 1;
            var updatedUser = DataGenerator.GetRandomUser();
            updatedUser.Id = userId;
            
            var endpoint = Endpoints.UserById.Replace("{id}", userId.ToString());

            // Act
            var response = await _apiClient.PutAsync<User>(endpoint, updatedUser);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Name.Should().Be(updatedUser.Name);
            response.Data.Email.Should().Be(updatedUser.Email);
        }

        [Test, Category("Regression")]
        public async Task DeleteUser_ShouldReturnSuccess()
        {
            // Arrange
            int userId = 1;
            var endpoint = Endpoints.UserById.Replace("{id}", userId.ToString());

            // Act
            var response = await _apiClient.DeleteAsync(endpoint);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}