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

namespace DrewBrasher.OrchardCore.ContentWarning.Drivers;
internal class ContentWarningContentDisplayDriver : ContentDisplayDriver
{
    public override Task<IDisplayResult> DisplayAsync(ContentItem model, BuildDisplayContext context)
    {
        var shape = context.Shape;
        // If the content item contains FormPart add Form Wrapper only in Display type Detail
        var part = model.As<ContentWarningPart>();
        if(part != null && part.ShowWarning)
        {
            // Add wrapper for content type if template is not available it will fall back to Form_Wrapper
            shape.Metadata.Wrappers.Add($"ContentWarning_Wrapper__{model.ContentType}");
            shape.Metadata.Wrappers.Add($"ContentWarning_Wrapper");
        }

        // We don't need to return a shape result
        return Task.FromResult<IDisplayResult>(null);
    }
}
