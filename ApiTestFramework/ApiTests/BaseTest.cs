using RestSharp;
using Microsoft.Extensions.Configuration;

namespace ApiTestFramework.ApiTests
{
    [SetUpFixture]
    public abstract class BaseTest
    {
        private RestClient _client;
        private IConfiguration _config;
        protected string BaseUrl;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _config = builder.Build();
            BaseUrl = _config["ApiBaseUrl"] ?? "https://jsonplaceholder.typicode.com";
            
            var timeout = _config.GetValue("TimeoutSeconds", 30);
            var options = new RestClientOptions(BaseUrl)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            
            _client = new RestClient(options);
            Console.WriteLine($"[INFO] Тесты запущены на {BaseUrl}");
            Console.WriteLine($"[INFO] Таймаут: {timeout} секунд");
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            _client.Dispose();
            Console.WriteLine("[INFO] Завершение тестов");
        }
    }
}