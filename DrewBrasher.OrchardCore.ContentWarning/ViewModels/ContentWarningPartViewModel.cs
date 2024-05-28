using DrewBrasher.OrchardCore.ContentWarning.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;

namespace DrewBrasher.OrchardCore.ContentWarning.ViewModels;
public class ContentWarningPartViewModel
{
    public string? WarningMessage { get; set; }

    public bool ShowWarning { get; set; }

    [BindNever]
    public ContentItem? ContentItem { get; set; }

    [BindNever]
    public ContentWarningPart? ContentWarningPart { get; set; }
}
