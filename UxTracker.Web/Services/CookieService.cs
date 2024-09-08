using Microsoft.JSInterop;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Services;

namespace UxTracker.Web.Services;

public class CookieService(IJSRuntime js): ICookieService
{
    public async Task<IEnumerable<Cookie>> GetAllAsync()
    {
        var raw = await js.InvokeAsync<string>("eval", "document.cookie");
        if (string.IsNullOrWhiteSpace(raw)) return Enumerable.Empty<Cookie>();

        return raw.Split("; ").Select(x =>
        {
            var parts = x.Split("=");
            if (parts.Length != 2) throw new Exception($"O formato do Cookie é inválido: '{x}'.");
            return new Cookie(parts[0], parts[1]);
        });
    }

    public async Task<Cookie?> GetAsync(string key)
    {
        var cookies = await GetAllAsync();
        return cookies.FirstOrDefault(x => x.Key == key);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (string.IsNullOrWhiteSpace(key)) throw new Exception("A chave é obrigatória quando se quer remover um cookie.");
        await js.InvokeVoidAsync("eval", cancellationToken, $"document.cookie = \"{key}=; expires=Thu, 01 Jan 1970 00:00:01 GMT; path=/\"");
    }

    public async Task SetAsync(string key, string value, DateTimeOffset? expiration,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        if (string.IsNullOrWhiteSpace(key)) throw new Exception("A chave é obrigatória quando se quer criar um cookie.");
        await js.InvokeVoidAsync("eval", cancellationToken, $"document.cookie = \"{key}={value}; expires={expiration:R}; path=/\"");
    }

    public async Task SetAsync(Cookie cookie, CancellationToken cancellationToken = default(CancellationToken))
    {
        await SetAsync(cookie.Key, cookie.Value, cookie.Expiration, cancellationToken);
    }
}