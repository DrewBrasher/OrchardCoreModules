using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "OCRazorModuleDemo",
    Author = "Drew Brasher",
    Website = "https://github.com/DrewBrasher/OrchardCoreModules",
    Version = "0.0.1",
    Description = "Module for testing DrewBrasher.OrchardCore.Blazor. Go to /OCRazorModuleDemo/Home/Index to test.",
    Dependencies =
    [
        "DrewBrasher.OrchardCore.Blazor"
    ],
    Category = "Blazor"
)]