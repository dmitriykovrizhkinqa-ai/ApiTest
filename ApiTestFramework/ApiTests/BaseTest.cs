using RestSharp;
using Microsoft.Extensions.Configuration;

namespace ApiTestFramework.ApiTests
{
    [SetUpFixture]
    public abstract class BaseTest
    {
        protected RestClient Client;
        protected string BaseUrl;
        protected IConfiguration Config;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Читаем конфиг из appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Config = builder.Build();
            BaseUrl = Config["ApiBaseUrl"] ?? "https://jsonplaceholder.typicode.com";
            
            // Таймаут из конфига
            var timeout = Config.GetValue<int>("TimeoutSeconds", 30);
            var options = new RestClientOptions(BaseUrl)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            
            Client = new RestClient(options);
            Console.WriteLine($"[INFO] Тесты запущены на {BaseUrl}");
            Console.WriteLine($"[INFO] Таймаут: {timeout} секунд");
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            Client?.Dispose();
            Console.WriteLine("[INFO] Завершение тестов");
        }
    }
}