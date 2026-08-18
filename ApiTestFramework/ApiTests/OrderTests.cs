using ApiTestFramework.Core;
using ApiTestFramework.Models;
using ApiTestFramework.Helpers;
using FluentAssertions;

namespace ApiTestFramework.ApiTests
{
    [TestFixture]
    public class OrderTests : BaseTest
    {
        private ApiClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new ApiClient(BaseUrl);
        }

        [Test, Category("Smoke")]
        public async Task GetOrders_ShouldReturnSuccess()
        {
            var endpoint = Endpoints.Orders;
            var response = await _apiClient.GetAsync<Order[]>(endpoint);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data.Length.Should().BeGreaterThan(0);
        }

        [Test, Category("Smoke")]
        public async Task GetOrderById_ShouldReturnCorrectOrder()
        {
            int orderId = 1;
            var endpoint = Endpoints.OrderById.Replace("{id}", orderId.ToString());

            var response = await _apiClient.GetAsync<Order>(endpoint);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Id.Should().Be(orderId);
            response.Data.Title.Should().NotBeNullOrEmpty();
        }

        [Test, Category("Regression")]
        public async Task CreateOrder_ShouldReturnCreated()
        {
            var newOrder = DataGenerator.GetRandomOrder();
            var response = await _apiClient.PostAsync<Order>(Endpoints.Orders, newOrder);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Data.Should().NotBeNull();
            response.Data.Id.Should().BeGreaterThan(0);
        }
    }
}