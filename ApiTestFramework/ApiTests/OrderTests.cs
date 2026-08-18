using Allure.NUnit;
using Allure.NUnit.Attributes;
using System.Net;
using ApiTestFramework.Core;
using ApiTestFramework.Models;
using ApiTestFramework.Helpers;
using FluentAssertions;

namespace ApiTestFramework.ApiTests
{
    [TestFixture]
    [AllureNUnit]
    [AllureFeature("Основная функциональность")]
    [AllureParentSuite("API Тесты")]
    public class OrderTests : BaseTest
    {
        private ApiClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new ApiClient(BaseUrl);
        }

        [Test]
        [AllureTag("Smoke")]
        [AllureDescription("Проверяем получение всех заказов")]
        public async Task GetOrders_ShouldReturnSuccess()
        {
            const string endpoint = Endpoints.Orders;
            var response = await _apiClient.GetAsync<Order[]>(endpoint);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data.Length.Should().BeGreaterThan(0);
        }

        [Test]
        [AllureTag("Smoke")]
        [AllureDescription("Проверяем получение заказа по id")]
        public async Task GetOrderById_ShouldReturnCorrectOrder()
        {
            var endpoint = Endpoints.OrderById(Constants.Id.ToString());
            var response = await _apiClient.GetAsync<Order>(endpoint);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data.Id.Should().Be(Constants.Id);
            response.Data.Title.Should().NotBeNullOrEmpty();
        }

        [Test]
        [AllureTag("Regression")]
        [AllureDescription("Проверяем создание заказа")]
        public async Task CreateOrder_ShouldReturnCreated()
        {
            var newOrder = DataGenerator.GetRandomOrder();
            var response = await _apiClient.PostAsync<Order>(Endpoints.Orders, newOrder);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Data.Should().NotBeNull();
            response.Data.Id.Should().BeGreaterThan(0);
        }
    }
}