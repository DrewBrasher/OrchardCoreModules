using DrewBrasher.OrchardCore.ContentWarning.Models;
using OrchardCore.ContentManagement.Handlers;
using System.Threading.Tasks;

namespace DrewBrasher.OrchardCore.ContentWarning.Handlers;
public class ContentWarningPartHandler : ContentPartHandler<ContentWarningPart>
{
    public override Task InitializingAsync(InitializingContentContext context, ContentWarningPart part)
    {
        part.Show = true;

        return Task.CompletedTask;
    }
}