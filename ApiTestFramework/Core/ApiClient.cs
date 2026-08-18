using RestSharp;
using ApiTestFramework.Helpers;

namespace ApiTestFramework.Core
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            _client = new RestClient(options);
        }

        public async Task<RestResponse<T>> GetAsync<T>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            request.AddHeader("Accept", "application/json");
            
            Logger.LogRequest("GET", endpoint);
            var response = await _client.ExecuteAsync<T>(request);
            Logger.LogResponse(response);
            
            return response;
        }

        public async Task<RestResponse<T>> PostAsync<T>(string endpoint, object body) where T : new()
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(body);
            request.AddHeader("Content-Type", "application/json");
            
            Logger.LogRequest("POST", endpoint, body);
            var response = await _client.ExecuteAsync<T>(request);
            Logger.LogResponse(response);
            
            return response;
        }

        public async Task<RestResponse<T>> PutAsync<T>(string endpoint, object body) where T : new()
        {
            var request = new RestRequest(endpoint, Method.Put);
            request.AddJsonBody(body);
            
            Logger.LogRequest("PUT", endpoint, body);
            var response = await _client.ExecuteAsync<T>(request);
            Logger.LogResponse(response);
            
            return response;
        }

        public async Task<RestResponse> DeleteAsync(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            
            Logger.LogRequest("DELETE", endpoint);
            var response = await _client.ExecuteAsync(request);
            Logger.LogResponse(response);
            
            return response;
        }
    }
}