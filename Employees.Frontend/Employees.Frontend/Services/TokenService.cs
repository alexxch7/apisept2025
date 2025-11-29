using System;

namespace Employees.Frontend.Services
{
    public class TokenService
    {
        public string Token { get; private set; } = string.Empty;

        public bool HasToken => !string.IsNullOrWhiteSpace(Token);

        public event Action? OnChange;

        public void SetToken(string token)
        {
            Token = token ?? string.Empty;
            OnChange?.Invoke();
        }

        public void ClearToken()
        {
            Token = string.Empty;
            OnChange?.Invoke();
        }
    }
}
