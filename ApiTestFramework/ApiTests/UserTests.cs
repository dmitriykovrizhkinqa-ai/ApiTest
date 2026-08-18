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
            const string endpoint = Endpoints.Users;
            
            var response = await _apiClient.GetAsync<User[]>(endpoint);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data.Length.Should().BeGreaterThan(0);
            
            Logger.LogInfo($"Получено {response.Data.Length} пользователей");
        }

        [Test, Category("Smoke")]
        public async Task GetUserById_ShouldReturnCorrectUser()
        {
            const int userId = 1;
            
            var endpoint = Endpoints.UserById(userId.ToString());
            var response = await _apiClient.GetAsync<User>(endpoint);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data.Id.Should().Be(userId);
            response.Data.Name.Should().NotBeNullOrEmpty();
        }

        [Test, Category("Regression")]
        public async Task CreateUser_ShouldReturnCreated()
        {
            var newUser = DataGenerator.GetRandomUser();
            var response = await _apiClient.PostAsync<User>(Endpoints.Users, newUser);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Data.Should().NotBeNull();
            response.Data.Id.Should().BeGreaterThan(0);
            response.Data.Name.Should().Be(newUser.Name);
            response.Data.Email.Should().Be(newUser.Email);
        }

        [Test, Category("Regression")]
        public async Task UpdateUser_ShouldReturnSuccess()
        {
            var updatedUser = DataGenerator.GetRandomUser();
            updatedUser.Id = Constants.Id;
            
            var endpoint = Endpoints.UserById(Constants.Id.ToString());
            var response = await _apiClient.PutAsync<User>(endpoint, updatedUser);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data.Name.Should().Be(updatedUser.Name);
            response.Data.Email.Should().Be(updatedUser.Email);
        }

        [Test, Category("Regression")]
        public async Task DeleteUser_ShouldReturnSuccess()
        {
            var endpoint = Endpoints.UserById(Constants.Id.ToString());
            var response = await _apiClient.DeleteAsync(endpoint);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}