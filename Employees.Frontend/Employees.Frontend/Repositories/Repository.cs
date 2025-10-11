using System.Net.Http.Json;

namespace Employees.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _http;
        public Repository(HttpClient http) => _http = http;

        public async Task<Response<T>> GetAsync<T>(string url)
        {
            var httpResp = await _http.GetAsync(url);
            if (!httpResp.IsSuccessStatusCode)
                return new Response<T> { Error = true, HttpResponseMessage = httpResp };

            var data = await httpResp.Content.ReadFromJsonAsync<T>();
            return new Response<T> { Error = false, Result = data, HttpResponseMessage = httpResp };
        }

        public async Task<Response<string>> GetStringAsync(string url)
        {
            var httpResp = await _http.GetAsync(url);
            var text = await httpResp.Content.ReadAsStringAsync();
            return new Response<string> { Error = !httpResp.IsSuccessStatusCode, Result = text, HttpResponseMessage = httpResp };
        }
    }
}
