using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Services;

public interface ICookieService
{
    public Task<IEnumerable<Cookie>> GetAllAsync();

    public Task<Cookie?> GetAsync(string key);

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default (CancellationToken));

    public Task SetAsync(
        string key,
        string value,
        DateTimeOffset? expiration,
        CancellationToken cancellationToken = default (CancellationToken));

    public Task SetAsync(Cookie cookie, CancellationToken cancellationToken = default (CancellationToken));
}