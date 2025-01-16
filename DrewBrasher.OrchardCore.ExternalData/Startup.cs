using DrewBrasher.OrchardCore.ExternalData.Shortcodes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Shortcodes;

namespace DrewBrasher.OrchardCore.ExternalData;
public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddShortcode<ExternalContentShortcodeProvider>("external_content", describe =>
        {
            describe.DefaultValue = "[external_content url=''][/external_content]";
            describe.Hint = "Displays content from an external source.";
            describe.Usage = @"[external_content url='' renderMode='MarkdownToHtml'][/external_content]
Arguments:
 - url: The url of the external content
 - renderMode (optional): How to render the content. Defaults to raw data if not specified.
     Supported values: MarkdownToHtml

<b>Warning: You should only embed content like this for URLs that you have full control over.</b>";
            describe.Categories = ["HTML Content"];
        });
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
    }
}

