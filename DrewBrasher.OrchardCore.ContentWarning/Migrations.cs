using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace DrewBrasher.OrchardCore.ContentWarning;
public class Migrations : DataMigration
{
    IContentDefinitionManager _contentDefinitionManager;

    public Migrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public int Create()
    {
        _contentDefinitionManager.AlterPartDefinition("ContentWarningPart", builder => builder
            .Attachable()
            .WithDescription("Provides a ContentWarning part for your content item."));

        return 1;
    }
}
