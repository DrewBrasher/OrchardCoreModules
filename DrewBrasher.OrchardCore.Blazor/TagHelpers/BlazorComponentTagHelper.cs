using DrewBrasher.OrchardCore.Blazor.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace DrewBrasher.OrchardCore.Blazor.TagHelpers;

/// <summary>
///     A TagHelper that renders a Blazor component and ensures that the <see cref="EnableBlazorComponentsTagHelper"/> is also rendered
/// </summary>
[HtmlTargetElement("blazor-component", TagStructure = TagStructure.WithoutEndTag)]
public class BlazorComponentTagHelper : TagHelper
{
    private readonly ICompositeViewEngine _viewEngine;
    private readonly ITempDataDictionaryFactory _tempDataFactory;
    private readonly HtmlHelperOptions _htmlHelperOptions;

    public BlazorComponentTagHelper(
        ICompositeViewEngine viewEngine,
        ITempDataDictionaryFactory tempDataFactory,
        IOptions<HtmlHelperOptions> htmlHelperOptionsAccessor)
    {
        _viewEngine = viewEngine;
        _tempDataFactory = tempDataFactory;
        _htmlHelperOptions = htmlHelperOptionsAccessor.Value;
    }

    /// <summary>
    ///     The rendering mode for the Blazor component.
    /// </summary>
    [HtmlAttributeName("render-mode")]
    public RenderMode RenderMode { get; set; }

    /// <summary>
    ///     The Orchard Core Zone inside the page <head></head>. This is used to render the <see cref="Microsoft.AspNetCore.Components.Web.HeadOutlet"/> and blazor.server.js.
    ///     Default: "HeadMeta".
    /// </summary>
    [HtmlAttributeName("header-zone")]
    public string HeaderZone { get; set; } = "HeadMeta";

    /// <summary>
    ///     The type of the Blazor component to render.
    /// </summary>
    [HtmlAttributeName("type")]
    public required Type ComponentType { get; set; }

    /// <summary>
    ///     The current view context.
    /// </summary>
    /// <remarks>
    ///     This is automatically set by the framework and provides the context for rendering the partial view.
    /// </remarks>
    [ViewContext]
    [HtmlAttributeNotBound]
    public required ViewContext ViewContext { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;

        var viewData = new ViewDataDictionary<BlazorComponentViewModel>(
            ViewContext.ViewData,
            new BlazorComponentViewModel(RenderMode, HeaderZone, ComponentType));

        using var writer = new StringWriter();
        var partialViewResult = _viewEngine.FindView(ViewContext, "_BlazorComponent", isMainPage: false);
        if (partialViewResult.Success)
        {
            var view = partialViewResult.View;
            var tempData = _tempDataFactory.GetTempData(ViewContext.HttpContext);
            var viewContext = new ViewContext(ViewContext, view, viewData, tempData, writer, _htmlHelperOptions);
            await view.RenderAsync(viewContext);
            output.Content.SetHtmlContent(writer.ToString());
        }
        else
        {
            throw new InvalidOperationException("Partial view '_BlazorComponent' not found.");
        }
    }
}