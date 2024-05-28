using DrewBrasher.OrchardCore.ContentWarning.Models;
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

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(ContentWarningPart), builder => builder
            .Attachable()
            .WithDescription("Allows you to put a content warning around a content item.")
            .WithDisplayName("Content Warning"));

        return 1;
    }
}
