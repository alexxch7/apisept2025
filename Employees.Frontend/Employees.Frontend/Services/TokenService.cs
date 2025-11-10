namespace Employees.Frontend.Services;

public class TokenService
{
    private string? _token;
    public const string StorageKey = "app_token";
    public void Save(string token) => _token = token;
    public string? GetToken() => _token;
    public void Clear() => _token = null;
}
