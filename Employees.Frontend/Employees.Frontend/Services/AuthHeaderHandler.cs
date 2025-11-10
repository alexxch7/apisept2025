using System.Net.Http.Headers;

namespace Employees.Frontend.Services;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly TokenService _tokens;

    public AuthHeaderHandler(TokenService tokens)
    {
        _tokens = tokens;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = _tokens.GetToken();
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
