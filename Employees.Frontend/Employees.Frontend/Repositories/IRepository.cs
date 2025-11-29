using System.Net.Http;

namespace Employees.Frontend.Repositories
{
    public interface IRepository
    {
        Task<Response<T>> GetAsync<T>(string url);
        Task<Response<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest model);
        Task<Response<string>> PutAsync<TRequest>(string url, TRequest model);
        Task<Response<string>> DeleteAsync(string url);
    }

    public class Response<T>
    {
        public bool Error { get; set; }
        public T? Result { get; set; }
        public HttpResponseMessage? HttpResponseMessage { get; set; }
    }
}
