using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace DrewBrasher.OrchardCore.ContentWarning;
public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
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
