using DrewBrasher.OrchardCore.ContentWarning.Models;
using DrewBrasher.OrchardCore.ContentWarning.Settings;
using DrewBrasher.OrchardCore.ContentWarning.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace DrewBrasher.OrchardCore.ContentWarning.Drivers;
public class ContentWarningPartDisplayDriver : ContentPartDisplayDriver<ContentWarningPart>
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public ContentWarningPartDisplayDriver(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public override IDisplayResult Display(ContentWarningPart part, BuildPartDisplayContext context)
    {
        return Initialize<ContentWarningPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
            .Location("Detail", "Content:10")
            .Location("Summary", "Content:10")
            ;
    }

    public override IDisplayResult Edit(ContentWarningPart part, BuildPartEditorContext context)
    {
        return Initialize<ContentWarningPartViewModel>(GetEditorShapeType(context), model =>
        {
            model.Show = part.Show;
            model.ContentItem = part.ContentItem;
            model.ContentWarningPart = part;
        });
    }

    public override async Task<IDisplayResult> UpdateAsync(ContentWarningPart model, IUpdateModel updater)
    {
        await updater.TryUpdateModelAsync(model, Prefix, t => t.Show);

        return Edit(model);
    }

    private static void BuildViewModel(ContentWarningPartViewModel model, ContentWarningPart part, BuildPartDisplayContext context)
    {
        var settings = context.TypePartDefinition.GetSettings<ContentWarningPartSettings>();

        model.ContentItem = part.ContentItem;
        model.MySetting = settings.MySetting;
        model.Show = part.Show;
        model.ContentWarningPart = part;
        model.Settings = settings;
    }
}
