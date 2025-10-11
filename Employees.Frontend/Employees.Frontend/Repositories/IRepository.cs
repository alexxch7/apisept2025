using System.Net.Http;

namespace Employees.Frontend.Repositories
{
    public interface IRepository
    {
        Task<Response<T>> GetAsync<T>(string url);
        Task<Response<string>> GetStringAsync(string url);
    }

    public class Response<T>
    {
        public bool Error { get; set; }
        public T? Result { get; set; }
        public HttpResponseMessage? HttpResponseMessage { get; set; }
    }
}
