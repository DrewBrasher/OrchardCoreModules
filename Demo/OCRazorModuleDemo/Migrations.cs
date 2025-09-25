using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCRazorModuleDemo;
public class Migrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public Migrations(IContentDefinitionManager contentDefinitionManager) =>
        _contentDefinitionManager = contentDefinitionManager;

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterTypeDefinitionAsync("DemoWidget", type => type
            .DisplayedAs("Demo Widget")
            .Creatable()
            .Listable()
            .Draftable()
            .Versionable()
            .Securable()
            .Stereotype("Widget")
            .WithPart("DemoWidget")
        );

        return 1;
    }
}
