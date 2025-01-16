# External Data Module
An Orchard Core CMS module to embed the content of external URLs in Markdown or HtmlBody, with a shortcode.

## NuGet Package
[![NuGet Version](https://img.shields.io/nuget/v/DrewBrasher.OrchardCore.ExternalData)](https://www.nuget.org/packages/DrewBrasher.OrchardCore.ExternalData/)

## Usage
**You should only embed content like this for URLs that you have full control over.**

### Getting Started
1. Add a reference to this module(`DrewBrasher.OrchardCore.ExternalData`) in your web application.
2. Enable the "External Data" feature in the Orchard Core Admin UI.

### External Content Shortcode
A shortcode for embedding the content of external URLs in Markdown or HtmlBody.
```
[external_content url='https://...' renderMode='MarkdownToHtml'][/external_content]
```
#### Arguments:
- url: The url of the external content
- renderMode (optional): How to render the content. Defaults to raw data if not specified.
Supported values: 
  - MarkdownToHtml: Renders markdown from the external source as HTML.

#### Example:
If you have a markdown file on GitHub with the url `https://{YourGitHubRepoUrl}/readme.md` that you would like to include in your Orchard Core blog post, you could put the shortcode in the MarkdownBody of your blog post:
```
[external_content url='https://{YourGitHubRepoUrl}/readme.md' renderMode='MarkdownToHtml'][/external_content]
```
The contents of the readme.md file would be rendered to HTML along with the rest of the MarkdownBody of your blog post.