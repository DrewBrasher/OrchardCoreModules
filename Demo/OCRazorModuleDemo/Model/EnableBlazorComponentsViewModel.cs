using Microsoft.AspNetCore.Mvc.Rendering;

namespace OCRazorModuleDemo.Model;

/// <summary>
///     Represents a view model for Blazor rendering modes.
/// </summary>
/// <remarks>
///     This view model encapsulates the Blazor <see cref="RenderMode"/> to specify how components should be rendered.
///     It provides predefined instances for static, server, and server-prerendered rendering modes.
/// </remarks>
public record EnableBlazorComponentsViewModel(RenderMode RenderMode)
{
    /// <summary> A view model configured for static rendering. </summary>
    public static EnableBlazorComponentsViewModel Static => new(RenderMode.Static);

    /// <summary> A view model configured for server rendering. </summary>
    public static EnableBlazorComponentsViewModel Server => new(RenderMode.Server);

    /// <summary> A view model configured for server-prerendered rendering. </summary>
    public static EnableBlazorComponentsViewModel ServerPrerendered => new(RenderMode.ServerPrerendered);
}