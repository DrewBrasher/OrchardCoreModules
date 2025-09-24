using DrewBrasher.OrchardCore.Blazor.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace DrewBrasher.OrchardCore.Blazor.TagHelpers;

/// <summary>
///     A TagHelper that renders the head sections and script for Blazor components inside an MVC or Razor Pages view.
/// </summary>
/// <remarks>
///     This TagHelper processes the <c>&lt;enable-blazor-components&gt;</c> tag and injects a Blazor component using the specified
///     <see cref="RenderMode"/>. It searches for a partial view named <c>_EnableBlazorComponents</c> and renders it asynchronously.
/// </remarks>
[HtmlTargetElement("enable-blazor-components", TagStructure = TagStructure.WithoutEndTag)]
public class EnableBlazorComponentsTagHelper : TagHelper
{
    private readonly ICompositeViewEngine _viewEngine;
    private readonly ITempDataDictionaryFactory _tempDataFactory;
    private readonly HtmlHelperOptions _htmlHelperOptions;

    /// <summary>
    ///     Initialises a new instance of the <see cref="BlazorfyTagHelper"/> class.
    /// </summary>
    /// <param name="viewEngine">The view engine used to locate the partial view.</param>
    /// <param name="tempDataFactory">The factory for creating TempData dictionaries.</param>
    /// <param name="htmlHelperOptionsAccessor">Options for HTML helper behaviour.</param>
    public EnableBlazorComponentsTagHelper(
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
    ///     The current view context.
    /// </summary>
    /// <remarks>
    ///     This is automatically set by the framework and provides the context for rendering the partial view.
    /// </remarks>
    [ViewContext]
    [HtmlAttributeNotBound]
    public required ViewContext ViewContext { get; set; }

    /// <summary>
    ///     Processes the TagHelper asynchronously, rendering the Blazor component inside the specified partial view.
    /// </summary>
    /// <param name="context">The context of the tag being processed.</param>
    /// <param name="output">The output where the generated HTML content is written.</param>
    /// <returns>A task that completes when the tag processing is finished.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the required partial view is not found.</exception>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        const string RenderedKey = "EnableBlazorComponents_Rendered";

        // Check if already rendered for this request
        if (ViewContext.HttpContext.Items.ContainsKey(RenderedKey))
        {
            // Suppress output if already rendered
            output.SuppressOutput();
            return;
        }

        // Mark as rendered
        ViewContext.HttpContext.Items[RenderedKey] = true;

        output.TagName = null;

        var viewData = new ViewDataDictionary<EnableBlazorComponentsViewModel>(
            ViewContext.ViewData,
            new EnableBlazorComponentsViewModel(RenderMode));

        using var writer = new StringWriter();
        var partialViewResult = _viewEngine.FindView(ViewContext, "_EnableBlazorComponents", isMainPage: false);
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
            throw new InvalidOperationException("Partial view '_EnableBlazorComponents' not found.");
        }
    }
}