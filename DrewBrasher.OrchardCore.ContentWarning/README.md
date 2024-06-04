# Content Warning Module
An Orchard Core CMS Module that allows you to put a content warning around some content so that users can decide if they want to see it or not.

There is a discussion about this module here: <https://github.com/OrchardCMS/OrchardCore/discussions/13643>

## NuGet Package
[![NuGet Version](https://img.shields.io/nuget/v/DrewBrasher.OrchardCore.ContentWarning)](https://www.nuget.org/packages/DrewBrasher.OrchardCore.ContentWarning/)


## Usage
### Getting Started
1. Add a reference to this module in your web application.
2. Enable the "Content Warning" feature.

### Shortcode
```
[content_warning 'warning-message'] some sensitive content [/content_warning]
```
or

```
[cw 'warning-message'] some sensitive content [/cw]
```

### Content Part
1. Add the Content Warning Part to any Content Types you would like to be able to put a warning around.
2. Check the "Show Warning" box and enter the warning message that you want the user to initially see instead of the content.

## Credits
The JavaScript and CSS are based on the <https://www.aaron-gustafson.com/notebook/considering-content-warnings-in-html/> blog post by [Aaron Gustafson](https://github.com/aarongustafson). 

I started this module during two Ochard Core Pair Programming by Lombiq sessions with [Zoltán Lehóczky](https://github.com/Piedone) that were live streamed. You can watch them here:

<https://www.youtube.com/watch?v=ECEbkLSOqLQ>

<https://www.youtube.com/watch?v=SmvLpAfyEHI>
