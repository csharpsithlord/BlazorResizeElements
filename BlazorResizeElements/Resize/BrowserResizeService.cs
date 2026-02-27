using Microsoft.JSInterop;

namespace BlazorResizeElements.Resize;

public sealed class BrowserResizeService : IAsyncDisposable
{
    private readonly IJSRuntime _js;
    private DotNetObjectReference<BrowserResizeService>? _dotNetRef;

    public BrowserSize Current { get; private set; } = new();

    public event EventHandler<BrowserSize>? Resized;

    public BrowserResizeService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitializeAsync()
    {
        _dotNetRef ??= DotNetObjectReference.Create(this);

        Current = await _js.InvokeAsync<BrowserSize>("browserResize.getSize");

        await _js.InvokeVoidAsync(
            "browserResize.register",
            _dotNetRef);
    }

    [JSInvokable]
    public void NotifyResize(double width, double height)
    {
        if (Current.Width == width && Current.Height == height)
            return;

        Current = new BrowserSize
        {
            Width = width,
            Height = height
        };

        Resized?.Invoke(this, Current);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await _js.InvokeVoidAsync("browserResize.unregister");
        }
        catch { }

        _dotNetRef?.Dispose();
    }
}
