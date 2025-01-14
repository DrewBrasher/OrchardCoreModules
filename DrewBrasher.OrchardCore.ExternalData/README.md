# External Data Module
An Orchard Core CMS Module that allows you to pull external data into your Orchard Core CMS site.

## Usage
### Getting Started
1. Add a reference to this module(`DrewBrasher.OrchardCore.ExternalData`) in your web application.
2. Enable the "External Data" feature in the Orchard Core Admin UI.

## NuGet Package
[![NuGet Version](https://img.shields.io/nuget/v/DrewBrasher.OrchardCore.ExternalData)](https://www.nuget.org/packages/DrewBrasher.OrchardCore.ExternalData/)

### External Content Shortcode
A shortcode for displaying content from an external source.
```
[external_content url='https://...' renderMode='MarkdownToHtml'][/external_content]
```
#### Arguments:
- url: The url of the external content
- renderMode (optional): How to render the content. Defaults to raw data if not specified.
Supported values: 
  - MarkdownToHtml: Renders markdown from the external source as HTML.
