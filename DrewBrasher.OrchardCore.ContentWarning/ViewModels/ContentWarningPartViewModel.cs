using DrewBrasher.OrchardCore.ContentWarning.Models;
using DrewBrasher.OrchardCore.ContentWarning.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;

namespace DrewBrasher.OrchardCore.ContentWarning.ViewModels;
public class ContentWarningPartViewModel
{
    public string MySetting { get; set; }

    public bool Show { get; set; }

    [BindNever]
    public ContentItem ContentItem { get; set; }

    [BindNever]
    public ContentWarningPart ContentWarningPart { get; set; }

    [BindNever]
    public ContentWarningPartSettings Settings { get; set; }
}
