namespace BlazorResizeElements.Resize;

public class BrowserSize
{
    public double Width { get; init; }
    public double Height { get; init; }

    public override string ToString()
        => $"{Width} x {Height}";
}
