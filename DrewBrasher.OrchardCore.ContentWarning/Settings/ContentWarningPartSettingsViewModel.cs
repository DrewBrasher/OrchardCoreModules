using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DrewBrasher.OrchardCore.ContentWarning.Settings;
public class ContentWarningPartSettingsViewModel
{
    public string MySetting { get; set; }

    [BindNever]
    public ContentWarningPartSettings ContentWarningPartSettings { get; set; }
}
