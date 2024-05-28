using OrchardCore.ContentManagement;

namespace DrewBrasher.OrchardCore.ContentWarning.Models;
public class ContentWarningPart : ContentPart
{
    public bool ShowWarning { get; set; }

    public string? WarningMessage { get; set; }
}
