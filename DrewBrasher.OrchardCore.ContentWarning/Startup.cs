using DrewBrasher.OrchardCore.ContentWarning.Drivers;
using DrewBrasher.OrchardCore.ContentWarning.Models;
using DrewBrasher.OrchardCore.ContentWarning.Shortcodes;
using DrewBrasher.OrchardCore.ContentWarning.ViewModels;
using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Infrastructure.Html;
using OrchardCore.Modules;
using OrchardCore.Shortcodes;

namespace DrewBrasher.OrchardCore.ContentWarning;
public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<TemplateOptions>(o =>
        {
            o.MemberAccessStrategy.Register<ContentWarningPartViewModel>();
        });

        services.AddContentPart<ContentWarningPart>()
            .UseDisplayDriver<ContentWarningPartDisplayDriver>();

		services.AddScoped<IContentDisplayDriver, ContentWarningContentDisplayDriver>();

		services.AddShortcode<ContentWarningShortcodeProvider>("content_warning", describe =>
        {
            describe.DefaultValue = "[content_warning ''] [/content_warning]";
            describe.Hint = "Hides some content with a warning message until the user choses to unhide it.";
            describe.Usage = "[content_warning 'Warning message'] Some sensitive content [/content_warning]<br />-- or --<br />[cw 'Warning message'] Some sensitive content [/cw]";
            describe.Categories = ["HTML Content"];
        });

        services.AddDataMigration<Migrations>();

        services.Configure<HtmlSanitizerOptions>(o =>
        {
            o.Configure.Add(new Action<Ganss.Xss.HtmlSanitizer>(o =>
            {
                o.AllowedAttributes.Add("data-content-warning");
            }));
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
