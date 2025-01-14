using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shortcodes;
using OrchardCore.Markdown.Services;

namespace DrewBrasher.OrchardCore.ExternalData.Shortcodes;
internal class ExternalContentShortcodeProvider : IShortcodeProvider
{
    private readonly IMarkdownService _markdownService;
    private readonly IHttpClientFactory _httpClientFactory;

    public ExternalContentShortcodeProvider(IMarkdownService markdownService, IHttpClientFactory httpClientFactory)
    {
        _markdownService = markdownService;
        _httpClientFactory = httpClientFactory;
    }

    public async ValueTask<string> EvaluateAsync(string identifier, Arguments arguments, string content, Context context)
    {
        var contentUrl = arguments.Named("url");

        if(!identifier.Equals("external_content", StringComparison.OrdinalIgnoreCase) || contentUrl == null)
        {
            return string.Empty;
        }

        using var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(contentUrl);
        content = response.Content.ReadAsStringAsync().Result;

        var renderMode = arguments.Named("renderMode");
        if(renderMode != null && renderMode.Equals("MarkdownToHtml", StringComparison.OrdinalIgnoreCase))
        {
            content = _markdownService.ToHtml(content ?? "");
            content = content.Replace("<img", "<img class=\"img-fluid\"");
        }

        return content;
    }
}
