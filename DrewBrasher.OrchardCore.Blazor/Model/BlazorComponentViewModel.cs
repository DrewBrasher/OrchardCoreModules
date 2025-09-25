using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrewBrasher.OrchardCore.Blazor.Model;

public class BlazorComponentViewModel
{
    public BlazorComponentViewModel(RenderMode renderMode, string headerZone, Type componentType)
    {
        RenderMode = renderMode;
        HeaderZone = headerZone;
        ComponentType = componentType;
    }

    public RenderMode RenderMode { get; }

    public string HeaderZone { get; set; }

    public Type ComponentType { get; set; }
}