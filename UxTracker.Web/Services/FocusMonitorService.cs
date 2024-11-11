using Microsoft.JSInterop;

namespace UxTracker.Web.Services;

public class FocusMonitorService : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private readonly DotNetObjectReference<FocusMonitorService> _dotNetRef;

    public event Action? OnFocusGained;

    public FocusMonitorService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _dotNetRef = DotNetObjectReference.Create(this);
    }

    public async Task InitializeAsync()
    {
        await _jsRuntime.InvokeVoidAsync("setupFocusEvent", _dotNetRef);
    }

    [JSInvokable]
    public void NotifyFocusGained()
    {
        OnFocusGained?.Invoke();
    }

    public async ValueTask DisposeAsync()
    {
        _dotNetRef?.Dispose();
        await Task.CompletedTask;
    }
}