namespace Employees.Frontend.Services;

public static class ApiClientExtensions
{
    public static HttpClient Api(this IHttpClientFactory f)
        => f.CreateClient("api");

    public static HttpClient ApiAuth(this IHttpClientFactory f)
        => f.CreateClient("api-auth");
}
