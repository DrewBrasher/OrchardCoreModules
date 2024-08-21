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
            .WithDescription("Allows you to hide a content item with a warning message until the user chooses to reveal it.")
            .WithDisplayName("Content Warning"));

        return 2;
    }

	public async Task<int> UpdateFrom1Async()
    {
		await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(ContentWarningPart), builder => builder
			.WithDescription("Allows you to hide a content item with a warning message until the user chooses to reveal it."));

		return 2;
    }
}
