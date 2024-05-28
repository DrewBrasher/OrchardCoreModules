using DrewBrasher.OrchardCore.ContentWarning.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Threading.Tasks;

namespace DrewBrasher.OrchardCore.ContentWarning.Settings;
public class ContentWarningPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
{
    public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
    {
        if(!string.Equals(nameof(ContentWarningPart), contentTypePartDefinition.PartDefinition.Name))
        {
            return null;
        }

        return Initialize<ContentWarningPartSettingsViewModel>("ContentWarningPartSettings_Edit", model =>
        {
            var settings = contentTypePartDefinition.GetSettings<ContentWarningPartSettings>();

            model.MySetting = settings.MySetting;
            model.ContentWarningPartSettings = settings;
        }).Location("Content");
    }

    public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
    {
        if(!string.Equals(nameof(ContentWarningPart), contentTypePartDefinition.PartDefinition.Name))
        {
            return null;
        }

        var model = new ContentWarningPartSettingsViewModel();

        if(await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.MySetting))
        {
            context.Builder.WithSettings(new ContentWarningPartSettings { MySetting = model.MySetting });
        }

        return Edit(contentTypePartDefinition, context.Updater);
    }
}
