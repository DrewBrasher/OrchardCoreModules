using OrchardCore.ResourceManagement;
using Shortcodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrewBrasher.OrchardCore.ContentWarning.Shortcodes;
internal class ContentWarningShortcodeProvider : IShortcodeProvider
{
    private readonly IResourceManager _resourceManager;
    private static readonly HashSet<string> _shortcodes = new(StringComparer.OrdinalIgnoreCase)
        {
            "content_warning",
            "cw"
        };
    public ContentWarningShortcodeProvider(IResourceManager resourceManager)
    {
        
        _resourceManager = resourceManager;
    }

    public ValueTask<string> EvaluateAsync(string identifier, Arguments arguments, string content, Context context)
    {
        if(!_shortcodes.Contains(identifier))
        {
            return new ValueTask<string>();
        }

        _resourceManager.RegisterUrl("script",
            "~/DrewBrasher.OrchardCore.ContentWarning/ContentWarning.js",
            "~/DrewBrasher.OrchardCore.ContentWarning/ContentWarning.js").AtFoot();

        _resourceManager.RegisterUrl("stylesheet",
            "~/DrewBrasher.OrchardCore.ContentWarning/ContentWarning.css",
            "~/DrewBrasher.OrchardCore.ContentWarning/ContentWarning.css");

        var warningMessage = arguments.NamedOrDefault("warning");
        content = $"<span data-content-warning=\"{warningMessage}\"><button class=\"btn btn-default btn-sm cw-rehide\" aria-label=\"re-hide content\"><i class=\"fa-solid fa-eye-slash\"></i></button>{content}</span>";
        return new ValueTask<string>(content);
    }
}
