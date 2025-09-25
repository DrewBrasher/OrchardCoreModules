using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrewBrasher.OrchardCore.Blazor.Model;

public class EnableBlazorComponentsViewModel
{
    public EnableBlazorComponentsViewModel(RenderMode renderMode, string headerZone)
    {
        RenderMode = renderMode;
        HeaderZone = headerZone;
    }

    public RenderMode RenderMode { get; }

    public string HeaderZone { get; set; }
}