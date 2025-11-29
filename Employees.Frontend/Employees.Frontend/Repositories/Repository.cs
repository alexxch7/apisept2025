using System.Net.Http;
using System.Net.Http.Json;

namespace Employees.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private HttpClient GetClient()
        {
            return _clientFactory.CreateClient("api-auth");
        }

        public async Task<Response<T>> GetAsync<T>(string url)
        {
            var client = GetClient();
            var httpResponse = await client.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new Response<T>
                {
                    Error = true,
                    HttpResponseMessage = httpResponse
                };
            }

            var result = await httpResponse.Content.ReadFromJsonAsync<T>();
            return new Response<T> { Result = result };
        }

        public async Task<Response<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest model)
        {
            var client = GetClient();
            var httpResponse = await client.PostAsJsonAsync(url, model);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new Response<TResponse>
                {
                    Error = true,
                    HttpResponseMessage = httpResponse
                };
            }

            var result = await httpResponse.Content.ReadFromJsonAsync<TResponse>();
            return new Response<TResponse> { Result = result };
        }

        public async Task<Response<string>> PutAsync<TRequest>(string url, TRequest model)
        {
            var client = GetClient();
            var httpResponse = await client.PutAsJsonAsync(url, model);

            return new Response<string>
            {
                Error = !httpResponse.IsSuccessStatusCode,
                HttpResponseMessage = httpResponse
            };
        }

        public async Task<Response<string>> DeleteAsync(string url)
        {
            var client = GetClient();
            var httpResponse = await client.DeleteAsync(url);

            return new Response<string>
            {
                Error = !httpResponse.IsSuccessStatusCode,
                HttpResponseMessage = httpResponse
            };
        }
    }
}
