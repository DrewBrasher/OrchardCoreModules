using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Content Warning",
    Author = "Drew Brasher",
    Website = "https://github.com/DrewBrasher/OrchardCoreModules",
    Version = "1.1.0",
    Description = "An Orchard Core CMS Module that allows you to hide some content with a warning message so that users can decide if they want to reveal it or not.",
    Dependencies = ["OrchardCore.Contents"],
    Category = "Content Management"
)]
