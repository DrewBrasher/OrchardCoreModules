using DrewBrasher.OrchardCore.ContentWarning.Shortcodes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Shortcodes;

namespace DrewBrasher.OrchardCore.ContentWarning;
public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddShortcode<ContentWarningShortcodeProvider>("content_warning", describe =>
        {
            describe.DefaultValue = "[content_warning 'warning-message'] [/content_warning]";
            describe.Hint = "Add a content warning to around some content.";
            describe.Usage = "[content_warning 'warning-message'] some sensitive content [/content_warning]";
            describe.Categories = new string[] { "HTML Content" };
        });
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "Home",
            areaName: "DrewBrasher.OrchardCore.ContentWarning",
            pattern: "Home/Index",
            defaults: new { controller = "Home", action = "Index" }
        );
    }
}
