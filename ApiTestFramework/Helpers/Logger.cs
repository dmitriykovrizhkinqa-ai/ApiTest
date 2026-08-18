using RestSharp;
using Newtonsoft.Json;

namespace ApiTestFramework.Helpers
{
    public static class Logger
    {
        public static void LogRequest(string method, string endpoint, object body = null)
        {
            Console.WriteLine($"[REQUEST] {method} {endpoint}");
            if (body != null)
            {
                Console.WriteLine($"[BODY] {JsonConvert.SerializeObject(body, Formatting.Indented)}");
            }
        }

        public static void LogResponse(RestResponse response)
        {
            Console.WriteLine($"[RESPONSE] Status: {(int)response.StatusCode} {response.StatusCode}");
            Console.WriteLine($"[RESPONSE] Body: {response.Content}");
            Console.WriteLine(new string('-', 80));
        }

        public static void LogInfo(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }
    }
}