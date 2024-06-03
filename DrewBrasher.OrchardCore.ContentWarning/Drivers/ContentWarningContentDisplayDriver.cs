using Microsoft.SqlServer.Server;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrewBrasher.OrchardCore.ContentWarning.Models;
using OrchardCore.ResourceManagement;

namespace DrewBrasher.OrchardCore.ContentWarning.Drivers;
internal class ContentWarningContentDisplayDriver : ContentDisplayDriver
{
    private readonly IResourceManager _resourceManager;

    public ContentWarningContentDisplayDriver(IResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
    }

    public override Task<IDisplayResult> DisplayAsync(ContentItem model, BuildDisplayContext context)
	{

		_resourceManager.RegisterUrl("script",
			"~/DrewBrasher.OrchardCore.ContentWarning/ContentWarning.js",
			"~/DrewBrasher.OrchardCore.ContentWarning/ContentWarning.js").AtFoot();

		_resourceManager.RegisterUrl("stylesheet",
			"~/DrewBrasher.OrchardCore.ContentWarning/ContentWarning.css",
			"~/DrewBrasher.OrchardCore.ContentWarning/ContentWarning.css");

		var shape = context.Shape;
        var part = model.As<ContentWarningPart>();
        if(part != null && part.ShowWarning)
        {
            shape.Metadata.Wrappers.Add($"ContentWarning_Wrapper__{model.ContentType}");
        }

        return Task.FromResult<IDisplayResult>(null);
    }
}
